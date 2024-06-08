using Core.Auth.Application.Attributes;
using MediatR;

namespace Products.Applications.Handlers.Queries.GetProductsCount;

[RequestAuthorize]
public class GetProductsCountQuery : ListProductFilter, IRequest<int>
{

}