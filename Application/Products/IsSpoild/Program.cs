using Auth.Application.Handlers.Commands.CreateJwtToken;
using Core.Products.Domain;
using Mails.Application.Handlers.Commands.SendMail;
using Microsoft.Extensions.Configuration;
using Products.Application.Handlers.Commands.UpdateProductMailTime;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Users.Application.Dtos;

public class SpoilCheck
{
    private static IConfiguration _configuration = new ConfigurationBuilder()
            .SetBasePath("C:\\Users\\iliuh\\source\\repos\\RestaurantSupply\\Application\\Products\\IsSpoild")
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();
    static HttpClient httpClient = new HttpClient();
    public static void Main()
    {       
        var mqService = _configuration.GetSection("MqService");
        var links = _configuration.GetSection("Links");
        var auth = _configuration.GetSection("Auth");
        var sendMail = _configuration.GetSection("SendMail");

        var factory = new ConnectionFactory
        {
            HostName = mqService.GetValue<string>("HostName"),
            UserName = mqService.GetValue<string>("UserName"),
            Password = mqService.GetValue<string>("Password")
        };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);

        var queueName = "spoiledProduct";
        channel.QueueBind(
            queue: queueName,
            exchange: "spoiledProductExchange",
            routingKey: string.Empty);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var product = JsonSerializer.Deserialize<Product>(message);

            var jwtToken = Autharization(auth.GetValue<string>("Login"), auth.GetValue<string>("Password"));

            //var serializedToken = JsonSerializer.Deserialize<JwtTokenDto>(jwtToken.Result);
            var serializedToken = jwtToken.Result.Split(@":")[1].Split("\"")[1];

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", serializedToken);

            var users = GetUsers(serializedToken);

            foreach (var user in users.Result)
            {
                var data = new SendMailCommand()
                {
                    Message = $"Product {product.Name} will spoil in ${product.SpoilTime}",
                    SenderMailAddress = sendMail.GetValue<string>("SenderMailAddress"), //e-mail
                    RecipientMailAddress = user.MailAddress,
                    RecipientName = user.Login,
                    Subject = "Spoiled product",
                    SenderPassword = sendMail.GetValue<string>("SenderPassword") //password
                };
                JsonContent content = JsonContent.Create(data);

                var httpRequest = new HttpRequestMessage(HttpMethod.Post, links.GetValue<string>("SendLink"));

                httpRequest.Content = content;

                var responce = httpClient.Send(httpRequest);

                ChangeMailTimeToUtcNow(product.ProductId);
            }

        };
        channel.BasicConsume(queue: queueName,
                             autoAck: false,
                             consumer: consumer);
        Console.ReadLine();
    }
    private static Task<string> Autharization(string login, string password)
    {
        var links = _configuration.GetSection("Links");

        var authData = new CreateJwtTokenCommand()
        {
            Login = login,
            Password = password
        };
        JsonContent content = JsonContent.Create(authData);

        var httpRequest = new HttpRequestMessage(HttpMethod.Post, links.GetValue<string>("AuthLink"));

        httpRequest.Content = content;

        var response = httpClient.Send(httpRequest);

        var responceMessage = response.Content.ReadAsStringAsync();

        return responceMessage;
    }
    private static Task<List<GetUserMailDto>> GetUsers(string token)
    {
        var links = _configuration.GetSection("Links");

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var httpRequest = new HttpRequestMessage(HttpMethod.Get, links.GetValue<string>("MailsLink"));

        var response = httpClient.Send(httpRequest);

        var responceMessage = response.Content.ReadFromJsonAsync<List<GetUserMailDto>>();

        return responceMessage;
    }

    private static void ChangeMailTimeToUtcNow(Guid guid)
    {
        var links = _configuration.GetSection("Links");

        var data = new UpdateProductMailTimeCommand()
        {
            ProductId = guid.ToString()
        };
        JsonContent content = JsonContent.Create(data);

        var httpRequest = new HttpRequestMessage(HttpMethod.Put, links.GetValue<string>("MailTimeLink") + $"{guid.ToString()}");

        httpRequest.Content = content;

        var response = httpClient.Send(httpRequest);

    }

}
