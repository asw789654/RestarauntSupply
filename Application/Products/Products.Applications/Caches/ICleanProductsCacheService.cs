namespace Products.Applications.Caches;

internal interface ICleanProductsCacheService
{
    void ClearAllCaches();
    void ClearListCaches();
}