namespace Mails.Application.Caches;

internal interface ICleanMailsCacheService
{
    void ClearAllCaches();
    void ClearListCaches();
}