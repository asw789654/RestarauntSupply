using Microsoft.Extensions.Caching.Distributed;
using Orders.Application.Caches;
using StackExchange.Redis;

namespace Infrastructure.DistributedCache.Orders;

public class OrdersCountMemoryCache : BaseCache<int>, IOrdersCountMemoryCache
{
    public OrdersCountMemoryCache(IDistributedCache distributedCache, RedisService redisServer) : base(distributedCache, redisServer)
    {
    }
}
