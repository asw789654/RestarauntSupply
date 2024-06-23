using Core.Application.Abstractions;
using Mails.Application.Caches;
using Mails.Application.DTOs;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace Infrastructure.DistributedCache.Mails;

public class MailsMemoryCache : BaseCache<GetMailDto>, IMailsMemoryCache
{
    public MailsMemoryCache(IDistributedCache distributedCache, RedisService redisServer) : base(distributedCache, redisServer)
    {
    }
}
