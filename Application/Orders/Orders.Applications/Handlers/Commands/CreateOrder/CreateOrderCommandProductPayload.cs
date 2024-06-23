using Core.Auth.Application.Attributes;

namespace Orders.Application.Handlers.Commands.CreateOrder;

[RequestAuthorize]
public class CreateOrderCommandProductPayload
{
    public string Name { get; set; } = default!;

    public int Volume { get; set; }

    public DateTime? SpoilTime { get; set; }
}