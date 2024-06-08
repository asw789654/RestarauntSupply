using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.BaseRealizations;
using Core.Auth.Application.Abstractions.Service;
using Orders.Applications.Caches;
using Orders.Domain;

namespace Orders.Applications.Handlers.Queries.GetOrdersCount;

internal class GetOrdersCountQueryHandler : BaseCashedForUserQuery<GetOrdersCountQuery, int>
{
    private readonly IBaseReadRepository<Order> _orders;

    private readonly ICurrentUserService _currentUserService;

    public GetOrdersCountQueryHandler(IBaseReadRepository<Order> orders, OrdersCountMemoryCache cache, ICurrentUserService currentUserService) : base(cache, currentUserService.CurrentUserId!.Value)
    {
        _orders = orders;
        _currentUserService = currentUserService;
    }

    public override async Task<int> SentQueryAsync(GetOrdersCountQuery request, CancellationToken cancellationToken)
    {
        return await _orders.AsAsyncRead().CountAsync(ListOrderWhere.WhereForAll(request), cancellationToken);
    }
}