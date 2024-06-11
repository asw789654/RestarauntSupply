namespace Mails.Applications.Caches;

internal class CleanMailsCacheService : ICleanMailsCacheService
{
    private readonly MailsMemoryCache _productMemoryCache;

    private readonly MailsListMemoryCache _mailsListMemoryCache;

    private readonly MailsCountMemoryCache _mailsCountMemoryCache;

    public CleanMailsCacheService(
        MailsMemoryCache productMemoryCache,
        MailsListMemoryCache mailsListMemoryCache,
        MailsCountMemoryCache mailsCountMemoryCache)
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