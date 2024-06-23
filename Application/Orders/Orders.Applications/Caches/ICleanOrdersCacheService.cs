namespace Orders.Application.Caches;

internal interface ICleanOrdersCacheService
{
    void ClearAllCaches();
    void ClearListCaches();
}