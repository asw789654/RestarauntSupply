using Core.Auth.Application.Attributes;
using Core.Products.Domain;
using MediatR;
using Orders.Applications.DTOs;

namespace Orders.Applications.Handlers.Commands.CreateOrder;

[RequestAuthorize]
public class CreateOrderCommand : IRequest<GetOrderDto>
{
    public List<Product> Products { get; set; } = default!;
}