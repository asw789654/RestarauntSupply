using Core.Auth.Application.Attributes;
using Core.Products.Domain;
using MediatR;
using Orders.Application.DTOs;

namespace Orders.Application.Handlers.Commands.CreateOrder;

[RequestAuthorize]
public class CreateOrderCommand : IRequest<GetOrderDto>
{
    public List<CreateOrderCommandProductPayload> Products { get; set; } = default!;
}