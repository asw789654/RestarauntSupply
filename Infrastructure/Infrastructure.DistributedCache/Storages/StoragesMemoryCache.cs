using Infrastructure.DistributedCache;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using Storages.Application.Caches;
using Storages.Application.DTOs;

namespace Infrastructure.DistributedCache.Storages;

public class StoragesMemoryCache : BaseCache<GetStorageDto>, IStoragesMemoryCache
{
    public StoragesMemoryCache(IDistributedCache distributedCache, RedisService redisServer) : base(distributedCache, redisServer)
    {
    }
}
