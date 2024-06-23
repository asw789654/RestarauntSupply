using Core.Application.Abstractions.Mappings;
using Core.Auth.Application.Attributes;
using MediatR;
using Orders.Application.DTOs;
using Orders.Domain;

namespace Orders.Application.Handlers.Commands.CancelOrder;

[RequestAuthorize]
public class CancelOrderCommand : IMapTo<Order>, IRequest<GetOrderDto>
{
    public string OrderId { get; set; } = default!;

}