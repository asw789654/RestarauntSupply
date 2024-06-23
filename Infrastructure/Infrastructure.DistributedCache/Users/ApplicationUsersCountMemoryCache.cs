using Microsoft.Extensions.Caching.Distributed;
using Users.Application.Caches;

namespace Infrastructure.DistributedCache.Users;

public class ApplicationUsersCountMemoryCache : BaseCache<int> , IApplicationUsersCountMemoryCache
{
    public ApplicationUsersCountMemoryCache(IDistributedCache distributedCache, RedisService redisServer) : base(distributedCache, redisServer)
    {
    }
}
