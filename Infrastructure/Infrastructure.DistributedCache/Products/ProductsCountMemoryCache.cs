using Microsoft.Extensions.Caching.Distributed;
using Products.Application.Caches;
using StackExchange.Redis;

namespace Infrastructure.DistributedCache.Products;

public class ProductsCountMemoryCache : BaseCache<int>, IProductsCountMemoryCache
{
    public ProductsCountMemoryCache(IDistributedCache distributedCache, RedisService redisServer) : base(distributedCache, redisServer)
    {
    }
}
