namespace Storages.Application.Caches;

internal class CleanStoragesCacheService : ICleanStoragesCacheService
{
    private readonly StoragesMemoryCache _storageMemoryCache;

    private readonly StoragesListMemoryCache _storagesListMemoryCache;

    private readonly StoragesCountMemoryCache _storagesCountMemoryCache;

    public CleanStoragesCacheService(
        StoragesMemoryCache storageMemoryCache,
        StoragesListMemoryCache storagesListMemoryCache,
        StoragesCountMemoryCache storagesCountMemoryCache)
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