
namespace Storages.Application.Caches;

internal class CleanStoragesCacheService : ICleanStoragesCacheService
{
    private readonly IStoragesMemoryCache _storageMemoryCache;

    private readonly IStoragesListMemoryCache _storagesListMemoryCache;

    private readonly IStoragesCountMemoryCache _storagesCountMemoryCache;

    public CleanStoragesCacheService(
        IStoragesMemoryCache storageMemoryCache,
        IStoragesListMemoryCache storagesListMemoryCache,
        IStoragesCountMemoryCache storagesCountMemoryCache)
    {
        _storageMemoryCache = storageMemoryCache;
        _storagesListMemoryCache = storagesListMemoryCache;
        _storagesCountMemoryCache = storagesCountMemoryCache;
    }

    public void ClearAllCaches()
    {
        _storageMemoryCache.Clear();
        _storagesListMemoryCache.Clear();
        _storagesCountMemoryCache.Clear();
    }

    public void ClearListCaches()
    {
        _storagesListMemoryCache.Clear();
        _storagesCountMemoryCache.Clear();
    }
}