using Core.Application.DTOs;
using Microsoft.Extensions.Caching.Distributed;
using Orders.Application.Caches;
using Orders.Application.DTOs;
using StackExchange.Redis;

namespace Infrastructure.DistributedCache.Orders;

public class OrdersListMemoryCache : BaseCache<BaseListDto<GetOrderDto>>, IOrdersListMemoryCache
{
    public OrdersListMemoryCache(IDistributedCache distributedCache, RedisService redisServer) : base(distributedCache, redisServer)
    {
    }
}
