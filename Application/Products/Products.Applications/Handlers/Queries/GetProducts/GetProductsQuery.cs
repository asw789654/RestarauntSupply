using Core.Application.DTOs;
using Core.Auth.Application.Attributes;
using MediatR;
using Products.Application.DTOs;
using Products.Application.Handlers.Queries;

namespace Products.Application.Handlers.Queries.GetProducts;

[RequestAuthorize]
public class GetProductsQuery : ListProductFilter, IBasePaginationFilter, IRequest<BaseListDto<GetProductDto>>
{
    public int? Limit { get; init; }

    public int? Offset { get; init; }
}