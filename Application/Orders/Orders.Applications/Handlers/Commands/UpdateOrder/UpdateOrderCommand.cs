using Core.Application.Abstractions.Mappings;
using Core.Auth.Application.Attributes;
using Core.Products.Domain;
using MediatR;
using Orders.Applications.DTOs;
using Orders.Domain;

namespace Orders.Applications.Handlers.Commands.UpdateOrder;

[RequestAuthorize]
public class UpdateOrderCommand : IMapTo<Order>, IRequest<GetOrderDto>
{
    public string OrderId { get; set; } = default!;

    public List<Product> Products { get; set; } = default!;
}