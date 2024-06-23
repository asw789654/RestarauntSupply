using Core.Application.DTOs;
using Infrastructure.DistributedCache;
using Microsoft.Extensions.Caching.Distributed;
using Products.Application.Caches;
using Products.Application.DTOs;
using StackExchange.Redis;

namespace Infrastructure.DistributedCache.Products;

public class ProductsListMemoryCache : BaseCache<BaseListDto<GetProductDto>>, IProductsListMemoryCache
{
    public ProductsListMemoryCache(IDistributedCache distributedCache, RedisService redisServer) : base(distributedCache, redisServer)
    {
    }
}
