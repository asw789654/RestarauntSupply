using Core.Application.DTOs;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using Users.Application.Caches;
using Users.Application.Dtos;

namespace Infrastructure.DistributedCache.Users;

public class ApplicationUsersListMemoryCache : BaseCache<BaseListDto<GetUserDto>> , IApplicationUsersListMemoryCache
{
    public ApplicationUsersListMemoryCache(IDistributedCache distributedCache, RedisService redisServer) : base(distributedCache, redisServer)
    {
    }
}
