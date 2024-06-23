using Core.Auth.Application.Attributes;
using MediatR;
using Orders.Application.Handlers.Queries;

namespace Orders.Application.Handlers.Queries.GetOrdersCount;

[RequestAuthorize]
public class GetOrdersCountQuery : ListOrderFilter, IRequest<int>
{

}