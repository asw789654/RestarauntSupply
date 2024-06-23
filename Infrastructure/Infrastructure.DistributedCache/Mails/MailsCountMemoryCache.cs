using Mails.Application.Caches;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace Infrastructure.DistributedCache.Mails;

public class MailsCountMemoryCache : BaseCache<int>, IMailsCountMemoryCache
{
    public MailsCountMemoryCache(IDistributedCache distributedCache, RedisService redisServer) : base(distributedCache, redisServer)
    {
    }
}
