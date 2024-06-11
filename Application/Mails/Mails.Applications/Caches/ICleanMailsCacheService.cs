namespace Mails.Applications.Caches;

internal interface ICleanMailsCacheService
{
    void ClearAllCaches();
    void ClearListCaches();
}