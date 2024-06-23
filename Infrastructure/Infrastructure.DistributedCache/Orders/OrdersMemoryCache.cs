using Infrastructure.DistributedCache;
using Microsoft.Extensions.Caching.Distributed;
using Orders.Application.Caches;
using Orders.Application.DTOs;
using StackExchange.Redis;

namespace Infrastructure.DistributedCache.Orders;

public class OrdersMemoryCache : BaseCache<GetOrderDto>, IOrdersMemoryCache
{
    public OrdersMemoryCache(IDistributedCache distributedCache, RedisService redisServer) : base(distributedCache, redisServer)
    {
    }
}
