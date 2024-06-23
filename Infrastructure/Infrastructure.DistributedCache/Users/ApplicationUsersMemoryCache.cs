using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using Users.Application.Caches;
using Users.Application.Dtos;

namespace Infrastructure.DistributedCache.Users;

public class ApplicationUsersMemoryCache : BaseCache<GetUserDto>, IApplicationUsersMemoryCache
{
    public ApplicationUsersMemoryCache(IDistributedCache distributedCache, RedisService redisServer) : base(distributedCache, redisServer)
    {
    }
}
