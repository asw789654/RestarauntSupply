using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace Infrastructure.DistributedCache;

public class RedisService
{
    private readonly IConfiguration _configuration;
    private ConnectionMultiplexer _redis;

    public IDatabase db { get; set; }

    public RedisService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public RedisKey[] GetAllKeys(string keyPrefix)
    {
        using (_redis = ConnectionMultiplexer.Connect(_configuration.GetConnectionString("Redis")))
        {
            var server = _redis.GetServers().Single();
            return server.Keys(pattern: keyPrefix + "*").ToArray();
        }
    }

    public IDatabase GetDb(int db)
    {
        return _redis.GetDatabase(db);
    }
}
