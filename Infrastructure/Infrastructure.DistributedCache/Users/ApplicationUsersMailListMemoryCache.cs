using Core.Application.DTOs;
using Microsoft.Extensions.Caching.Distributed;
using Users.Application.Caches;
using Users.Application.Dtos;

namespace Infrastructure.DistributedCache.Users;

public class ApplicationUsersMailListMemoryCache : BaseCache<BaseListDto<GetUserMailDto>> , IApplicationUsersMailListMemoryCache
{
    public ApplicationUsersMailListMemoryCache(IDistributedCache distributedCache, RedisService redisServer) : base(distributedCache, redisServer)
    {
    }
}
