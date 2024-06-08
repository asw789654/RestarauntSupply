namespace Core.Application.Abstractions;

public interface IMqService
{
    void SendMessage(string queue, string message);
}
