using Core.Application.DTOs;
using Core.Auth.Application.Attributes;
using MediatR;
using Orders.Applications.DTOs;
using Orders.Applications.Handlers.Queries;

namespace Orders.Applications.Handlers.Queries.GetOrders;

[RequestAuthorize]
public class GetOrdersQuery : ListOrderFilter, IBasePaginationFilter, IRequest<BaseListDto<GetOrderDto>>
{
    public int? Limit { get; init; }

    public int? Offset { get; init; }
}