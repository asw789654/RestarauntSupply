using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.BaseRealizations;
using Core.Application.DTOs;
using Core.Auth.Application.Abstractions.Service;
using Core.Products.Domain;
using Core.Users.Domain.Enums;
using MediatR;
using Products.Applications.Caches;
using Products.Applications.DTOs;
using Products.Applications.Handlers.Queries;

namespace Products.Applications.Handlers.Queries.GetProducts;

public class GetProductsQueryHandler : BaseCashedForUserQuery<GetProductsQuery, BaseListDto<GetProductDto>>
{
    private readonly IBaseReadRepository<Product> _products;

    private readonly ICurrentUserService _currentUserService;

    private readonly IMapper _mapper;

    public GetProductsQueryHandler(IBaseReadRepository<Product> products, ICurrentUserService currentUserService, IMapper mapper, ProductsListMemoryCache cache) : base(cache, currentUserService.CurrentUserId!.Value)
    {
        _products = products;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public override async Task<BaseListDto<GetProductDto>> SentQueryAsync(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var query = _products.AsQueryable();

        if (request.Offset.HasValue)
        {
            query = query.Skip(request.Offset.Value);
        }

        if (request.Limit.HasValue)
        {
            query = query.Take(request.Limit.Value);
        }

        query = query.Where(ListProductWhere.WhereForAll(request));

        query = query.OrderBy(e => e.ProductId);

        var items = await _products.AsAsyncRead().ToArrayAsync(query, cancellationToken);

        var totalCount = await _products.AsAsyncRead().CountAsync(query, cancellationToken);

        return new BaseListDto<GetProductDto>
        {
            TotalCount = totalCount,
            Items = _mapper.Map<GetProductDto[]>(items)
        };
    }
}