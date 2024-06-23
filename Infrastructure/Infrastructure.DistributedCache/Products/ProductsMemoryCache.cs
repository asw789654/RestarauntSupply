using Microsoft.Extensions.Caching.Distributed;
using Products.Application.Caches;
using Products.Application.DTOs;
using StackExchange.Redis;

namespace Infrastructure.DistributedCache.Products;

public class ProductsMemoryCache : BaseCache<GetProductDto>, IProductsMemoryCache
{
    public ProductsMemoryCache(IDistributedCache distributedCache, RedisService redisServer) : base(distributedCache, redisServer)
    {
    }
}
