using Auth.Application.Handlers.Commands.CreateJwtToken;
using Core.Products.Domain;
using Microsoft.Extensions.Configuration;
using Products.Application.Handlers.Commands.UpdateProductDelivered;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
public class IsDelivered
{
    private static IConfiguration _configuration = new ConfigurationBuilder()
            .SetBasePath("C:\\Users\\iliuh\\source\\repos\\RestaurantSupply\\Application\\Products\\IsDeliveredConsumer")
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();
    static HttpClient httpClient = new HttpClient();
    public static void Main()
    {
        var mqService = _configuration.GetSection("MqService");
        var links = _configuration.GetSection("Links");
        var auth = _configuration.GetSection("Auth");

        var factory = new ConnectionFactory
        {
            HostName = mqService.GetValue<string>("HostName"),
            UserName = mqService.GetValue<string>("UserName"),
            Password = mqService.GetValue<string>("Password")
        };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);

        var queueName = "addProductOnOrderComplete";
        channel.QueueBind(
            queue: queueName,
            exchange: "addProductOnOrderCompleteExchange",
            routingKey: string.Empty);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var orderId = JsonSerializer.Deserialize<string>(message);

            var jwtToken = Autharization(auth.GetValue<string>("Login"), auth.GetValue<string>("Password"));
            //var serializedToken = JsonSerializer.Deserialize<JwtTokenDto>(jwtToken.Result);
            var serializedToken = jwtToken.Result.Split(@":")[1].Split("\"")[1];

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", serializedToken);

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, links.GetValue<string>("ProductsLink"));

            var response = httpClient.Send(httpRequest);
            var responceMessage = response.Content.ReadFromJsonAsync<List<Product>>();
            foreach (var product in responceMessage.Result)
            {
                bool kek = product.OrderId == Guid.Parse(orderId);
                if (product.OrderId == Guid.Parse(orderId))
                {
                    ChangeDeliveredStatus(product.ProductId);
                }
            }
        };
        channel.BasicConsume(queue: "addProductOnOrderComplete",
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
    private static Task<string> ChangeDeliveredStatus(Guid productId)
    {
        var links = _configuration.GetSection("Links");

        var commandData = new UpdateProductDeliveredCommand()
        {
            ProductId = productId.ToString(),
        };

        JsonContent content = JsonContent.Create(commandData);
        var httpRequest = new HttpRequestMessage(HttpMethod.Put, links.GetValue<string>("IsDeliveredLink") + $"{productId}");
        // отправляем запрос
        httpRequest.Content = content;

        var response = httpClient.Send(httpRequest);
        // получаем ответ
        var responceMessage = response.Content.ReadAsStringAsync();

        return responceMessage;
    }
}