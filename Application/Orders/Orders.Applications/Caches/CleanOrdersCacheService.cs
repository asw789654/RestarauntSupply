namespace Orders.Applications.Caches;

internal class CleanOrdersCacheService : ICleanOrdersCacheService
{
    private readonly OrdersMemoryCache _orderMemoryCache;

    private readonly OrdersListMemoryCache _ordersListMemoryCache;

    private readonly OrdersCountMemoryCache _ordersCountMemoryCache;

    public CleanOrdersCacheService(
        OrdersMemoryCache orderMemoryCache,
        OrdersListMemoryCache ordersListMemoryCache,
        OrdersCountMemoryCache ordersCountMemoryCache)
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