using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.BaseRealizations;
using Core.Auth.Application.Abstractions.Service;
using Core.Products.Domain;
using Core.Users.Domain.Enums;
using Products.Applications.Caches;
using Products.Applications.Handlers.Queries;

namespace Products.Applications.Handlers.Queries.GetProductsCount;

internal class GetProductsCountQueryHandler : BaseCashedForUserQuery<GetProductsCountQuery, int>
{
    private readonly IBaseReadRepository<Product> _products;

    private readonly ICurrentUserService _currentUserService;

    public GetProductsCountQueryHandler(IBaseReadRepository<Product> products, ProductsCountMemoryCache cache, ICurrentUserService currentUserService) : base(cache, currentUserService.CurrentUserId!.Value)
    {
        _products = products;
        _currentUserService = currentUserService;
    }

    public override async Task<int> SentQueryAsync(GetProductsCountQuery request, CancellationToken cancellationToken)
    {
        return await _products.AsAsyncRead().CountAsync(ListProductWhere.WhereForAll(request), cancellationToken);
    }
}