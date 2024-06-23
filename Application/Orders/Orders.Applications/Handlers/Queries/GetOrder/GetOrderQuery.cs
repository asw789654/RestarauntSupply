using Core.Auth.Application.Attributes;
using MediatR;
using Orders.Application.DTOs;

namespace Orders.Application.Handlers.Queries.GetOrder;

[RequestAuthorize]
public class GetOrderQuery : IRequest<GetOrderDto>
{
    public string OrderId { get; init; } = default!;
}