using Auth.Application.Handlers.Commands.CreateJwtToken;
using Core.Products.Domain;
using Mails.Applications.Handlers.Commands.SendMail;
using Products.Applications.Handlers.Commands.UpdateProductMailTime;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Users.Application.Dtos;

public class SpoilCheck
{
    static HttpClient httpClient = new HttpClient();
    public static void Main()
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest"
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

            var jwtToken = Autharization("asw789654", "123456789");

            //var serializedToken = JsonSerializer.Deserialize<JwtTokenDto>(jwtToken.Result);
            var serializedToken = jwtToken.Result.Split(@":")[1].Split("\"")[1];

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", serializedToken);

            var users = GetUsers(serializedToken);

            foreach (var user in users.Result)
            {
                var data = new SendMailCommand()
                {
                    Message = $"Product {product.Name} will spoil in ${product.SpoilTime}",
                    SenderMailAddress = "", //e-mail
                    RecipientMailAddress = user.MailAddress,
                    RecipientName = user.Login,
                    Subject = "Spoiled product",
                    SenderPassword = "" //password
                };
                JsonContent content = JsonContent.Create(data);

                var httpRequest = new HttpRequestMessage(HttpMethod.Post, $"http://localhost:5199/api/v1/Mails/Send");

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
        var authData = new CreateJwtTokenCommand()
        {
            Login = login,
            Password = password
        };
        JsonContent content = JsonContent.Create(authData);

        var httpRequest = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5216/auth/api/v1/LoginJwt");

        httpRequest.Content = content;

        var response = httpClient.Send(httpRequest);

        var responceMessage = response.Content.ReadAsStringAsync();

        return responceMessage;
    }
    private static Task<List<GetUserMailDto>> GetUsers(string token)
    {
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:5234/UM/api/v1/Users/Mails");

        var response = httpClient.Send(httpRequest);

        var responceMessage = response.Content.ReadFromJsonAsync<List<GetUserMailDto>>();

        return responceMessage;
    }

    private static void ChangeMailTimeToUtcNow(Guid guid)
    {
        var data = new UpdateProductMailTimeCommand()
        {
            ProductId = guid.ToString()
        };
        JsonContent content = JsonContent.Create(data);

        var httpRequest = new HttpRequestMessage(HttpMethod.Put, $"http://localhost:5299/api/v1/Products/MailTime/{guid.ToString()}");

        httpRequest.Content = content;

        var response = httpClient.Send(httpRequest);

    }

}
