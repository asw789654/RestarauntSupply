using Core.Application.DTOs;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using Storages.Application.Caches;
using Storages.Application.DTOs;

namespace Infrastructure.DistributedCache.Storages;

public class StoragesListMemoryCache : BaseCache<BaseListDto<GetStorageDto>>, IStoragesListMemoryCache
{
    public StoragesListMemoryCache(IDistributedCache distributedCache, RedisService redisServer) : base(distributedCache, redisServer)
    {
    }
}
