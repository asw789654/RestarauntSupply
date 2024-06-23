using Auth.Application.Handlers.Commands.CreateJwtToken;
using Core.Products.Domain;
using Products.Application.Handlers.Commands.UpdateProductDelivered;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
public class IsDelivered
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

        var queueName = "addProductOnOrderCompleate";
        channel.QueueBind(
            queue: queueName,
            exchange: "addProductOnOrderCompleateExchange",
            routingKey: string.Empty);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var orderId = JsonSerializer.Deserialize<string>(message);

            var jwtToken = Autharization("asw789654", "123456789");
            //var serializedToken = JsonSerializer.Deserialize<JwtTokenDto>(jwtToken.Result);
            var serializedToken = jwtToken.Result.Split(@":")[1].Split("\"")[1];

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", serializedToken);

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:5299/api/v1/Products");

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
        channel.BasicConsume(queue: "addProductOnOrderCompleate",
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
    private static Task<string> ChangeDeliveredStatus(Guid productId)
    {
        var commandData = new UpdateProductDeliveredCommand()
        {
            ProductId = productId.ToString(),
        };

        JsonContent content = JsonContent.Create(commandData);
        var httpRequest = new HttpRequestMessage(HttpMethod.Put, $"http://localhost:5299/api/v1/Products/isDelivered/{productId}");
        // отправляем запрос
        httpRequest.Content = content;

        var response = httpClient.Send(httpRequest);
        // получаем ответ
        var responceMessage = response.Content.ReadAsStringAsync();

        return responceMessage;
    }
}