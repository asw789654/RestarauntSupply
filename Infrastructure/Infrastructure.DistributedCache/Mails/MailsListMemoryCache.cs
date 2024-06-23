using Core.Application.DTOs;
using Mails.Application.Caches;
using Mails.Application.DTOs;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace Infrastructure.DistributedCache.Mails;

public class MailsListMemoryCache : BaseCache<BaseListDto<GetMailDto>>, IMailsListMemoryCache
{
    public MailsListMemoryCache(IDistributedCache distributedCache, RedisService redisServer) : base(distributedCache, redisServer)
    {
    }
}
