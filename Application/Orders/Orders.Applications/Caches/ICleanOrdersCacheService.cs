namespace Orders.Applications.Caches;

internal interface ICleanOrdersCacheService
{
    void ClearAllCaches();
    void ClearListCaches();
}