using Core.Auth.Application.Attributes;
using MediatR;
using Orders.Applications.Handlers.Queries;

namespace Orders.Applications.Handlers.Queries.GetOrdersCount;

[RequestAuthorize]
public class GetOrdersCountQuery : ListOrderFilter, IRequest<int>
{

}