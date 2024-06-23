using Core.Auth.Application.Attributes;
using MediatR;
using Products.Application.Handlers.Queries;

namespace Products.Application.Handlers.Queries.GetProductsCount;

[RequestAuthorize]
public class GetProductsCountQuery : ListProductFilter, IRequest<int>
{

}