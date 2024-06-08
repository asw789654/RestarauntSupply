namespace Storages.Application.Caches;

internal interface ICleanStoragesCacheService
{
    void ClearAllCaches();
    void ClearListCaches();
}