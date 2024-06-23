using Core.Application.DTOs;
using Core.Auth.Application.Attributes;
using MediatR;
using Orders.Application.DTOs;
using Orders.Application.Handlers.Queries;

namespace Orders.Application.Handlers.Queries.GetOrders;

[RequestAuthorize]
public class GetOrdersQuery : ListOrderFilter, IBasePaginationFilter, IRequest<BaseListDto<GetOrderDto>>
{
    public int? Limit { get; init; }

    public int? Offset { get; init; }
}