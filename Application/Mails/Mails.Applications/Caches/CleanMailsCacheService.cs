namespace Mails.Application.Caches;

internal class CleanMailsCacheService : ICleanMailsCacheService
{
    private readonly IMailsMemoryCache _productMemoryCache;

    private readonly IMailsListMemoryCache _mailsListMemoryCache;

    private readonly IMailsCountMemoryCache _mailsCountMemoryCache;

    public CleanMailsCacheService(
        IMailsMemoryCache productMemoryCache,
        IMailsListMemoryCache mailsListMemoryCache,
        IMailsCountMemoryCache mailsCountMemoryCache)
    {
        _productMemoryCache = productMemoryCache;
        _mailsListMemoryCache = mailsListMemoryCache;
        _mailsCountMemoryCache = mailsCountMemoryCache;
    }

    public void ClearAllCaches()
    {
        _productMemoryCache.Clear();
        _mailsListMemoryCache.Clear();
        _mailsCountMemoryCache.Clear();
    }

    public void ClearListCaches()
    {
        _mailsListMemoryCache.Clear();
        _mailsCountMemoryCache.Clear();
    }
}