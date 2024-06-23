namespace Products.Application.Caches;

internal interface ICleanProductsCacheService
{
    void ClearAllCaches();
    void ClearListCaches();
}