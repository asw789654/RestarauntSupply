namespace Orders.Application.Caches;

internal class CleanOrdersCacheService : ICleanOrdersCacheService
{
    private readonly IOrdersMemoryCache _orderMemoryCache;

    private readonly IOrdersListMemoryCache _ordersListMemoryCache;

    private readonly IOrdersCountMemoryCache _ordersCountMemoryCache;

    public CleanOrdersCacheService(
        IOrdersMemoryCache orderMemoryCache,
        IOrdersListMemoryCache ordersListMemoryCache,
        IOrdersCountMemoryCache ordersCountMemoryCache)
    {
        _orderMemoryCache = orderMemoryCache;
        _ordersListMemoryCache = ordersListMemoryCache;
        _ordersCountMemoryCache = ordersCountMemoryCache;
    }

    public void ClearAllCaches()
    {
        _orderMemoryCache.Clear();
        _ordersListMemoryCache.Clear();
        _ordersCountMemoryCache.Clear();
    }

    public void ClearListCaches()
    {
        _ordersListMemoryCache.Clear();
        _ordersCountMemoryCache.Clear();
    }
}