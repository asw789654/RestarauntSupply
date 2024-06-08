using Core.Application.DTOs;
using Core.Auth.Application.Attributes;
using MediatR;
using Products.Applications.DTOs;

namespace Products.Applications.Handlers.Queries.GetProducts;

[RequestAuthorize]
public class GetProductsQuery : ListProductFilter, IBasePaginationFilter, IRequest<BaseListDto<GetProductDto>>
{
    public int? Limit { get; init; }

    public int? Offset { get; init; }
}