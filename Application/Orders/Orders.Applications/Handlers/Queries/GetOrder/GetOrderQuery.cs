using Core.Auth.Application.Attributes;
using MediatR;
using Orders.Applications.DTOs;

namespace Orders.Applications.Handlers.Queries.GetOrder;

[RequestAuthorize]
public class GetOrderQuery : IRequest<GetOrderDto>
{
    public string OrderId { get; init; } = default!;
}