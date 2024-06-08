using Core.Application.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text;

namespace Infrastracture.Mq;

public class MqService : IMqService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<MqService> _logger;
    public MqService(IConfiguration configuration, ILogger<MqService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }
    public void SendMessage(string queue, string message)
    {
        var factory = new ConnectionFactory 
        { 
            HostName = "localhost",
            Port = 15672,
            UserName = "guest",
            Password = "guest"
        };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(
            exchange: string.Empty,
            routingKey: queue,
            basicProperties: null,
            body: body);

        _logger.LogInformation($"[x] Sent {message}");
    }
}
