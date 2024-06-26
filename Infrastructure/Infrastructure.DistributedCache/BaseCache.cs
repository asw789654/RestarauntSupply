using Core.Application.Abstractions;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Infrastructure.DistributedCache;

public abstract class BaseCache<TItem> : IBaseCache<TItem>
{
    private readonly IDistributedCache _distributedCache;

    private readonly RedisService _redisServer;

    private string ItemName = typeof(TItem).Name;

    public BaseCache(IDistributedCache distributedCache, RedisService redisServer)
    {
        _distributedCache = distributedCache;
        _redisServer = redisServer;
    }
    protected virtual int AbsoluteExpiration => 10;

    protected virtual int SlidingExpiration => 5;

    private string CreateCacheKey<TRequest>(TRequest request)
    {
        return $"{ItemName}_{JsonSerializer.Serialize(request)}";
    }

    private string CreateCacheKey<TRequest>(TRequest request, string secondKey)
    {
        return $"{ItemName}_{JsonSerializer.Serialize(request)}_{secondKey}";
    }

    public void Set<TRequest>(TRequest request, string secondKey, TItem item, int size)
    {
        var jsonItem = JsonSerializer.Serialize(item);

        var cacheKey = CreateCacheKey(request, secondKey);

        _distributedCache.SetString(cacheKey, jsonItem,
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(AbsoluteExpiration),
                SlidingExpiration = TimeSpan.FromMinutes(SlidingExpiration)
            });
    }

    public void Set<TRequest>(TRequest request, TItem item, int size)
    {
        var jsonItem = JsonSerializer.Serialize(item);

        _distributedCache.SetString(
            CreateCacheKey(request),
            jsonItem,
            new DistributedCacheEntryOptions
            {
                //AbsoluteExpiration = new DateTimeOffset(DateTime.Now, new TimeSpan(0, 0, AbsoluteExpiration, 0)),
                SlidingExpiration = TimeSpan.FromMinutes(SlidingExpiration)
            });
    }

    public bool TryGetValue<TRequest>(TRequest request, out TItem? item)
    {
        var itemString = _distributedCache.GetString(CreateCacheKey(request));
        if (string.IsNullOrWhiteSpace(itemString))
        {
            item = default;
            return false;
        }
        item = JsonSerializer.Deserialize<TItem>(itemString, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = false
        });
        return true;
    }

    public bool TryGetValue<TRequest>(TRequest request, string secondKey, out TItem? item)
    {
        var itemString = _distributedCache.GetString(CreateCacheKey(request, secondKey));
        if (string.IsNullOrWhiteSpace(itemString))
        {
            item = default;
            return false;
        }
        item = JsonSerializer.Deserialize<TItem>(itemString, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = false
        });
        return true;
    }

    public void DeleteItem<TRequest>(TRequest request)
    {
        _distributedCache.Remove(CreateCacheKey(request));
    }

    public void DeleteItem<TRequest>(TRequest request, string secondKey)
    {
        _distributedCache.Remove(CreateCacheKey(request, secondKey));
    }

    public void Clear()
    {
        var keys = _redisServer.GetAllKeys(ItemName).ToArray();

        foreach (var redisKey in keys)
        {
            _distributedCache.Remove(redisKey);
        }

    }
}