using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

var factory = new ConnectionFactory
{
    HostName = "localhost",
    UserName = "guest",
    Password = "guest"
};
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);

var queueName = "SpoildProduct";
channel.QueueBind(
    queue: queueName,
    exchange: "SpoildProductExchange",
    routingKey: string.Empty);

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    var product = JsonSerializer.Deserialize<string>(message);
    Console.WriteLine(product);
    //var httpClient = new HttpClient();
    //var httpRequest = new HttpRequestMessage(HttpMethod.Put, $"http://localhost:5299/api/v1/Products/{product}/isDelivered");
    //var responceMessage = httpClient.Send(httpRequest);
};
channel.BasicConsume(queue: queueName,
                     autoAck: false,
                     consumer: consumer);
Console.ReadLine();
