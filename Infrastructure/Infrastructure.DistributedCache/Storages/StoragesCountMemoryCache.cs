using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using Storages.Application.Caches;

namespace Infrastructure.DistributedCache.Storages;

public class StoragesCountMemoryCache : BaseCache<int>, IStoragesCountMemoryCache
{
    public StoragesCountMemoryCache(IDistributedCache distributedCache, RedisService redisServer) : base(distributedCache, redisServer)
    {
    }
}
