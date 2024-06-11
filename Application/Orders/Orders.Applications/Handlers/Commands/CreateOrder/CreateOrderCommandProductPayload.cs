using Core.Auth.Application.Attributes;
using Core.Products.Domain;
using MediatR;
using Orders.Applications.DTOs;

namespace Orders.Applications.Handlers.Commands.CreateOrder;

[RequestAuthorize]
public class CreateOrderCommandProductPayload
{
    public string Name { get; set; } = default!;

    public int Volume { get; set; }

    public DateTime? SpoilTime { get; set; }
}