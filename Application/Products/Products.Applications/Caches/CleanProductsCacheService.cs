namespace Products.Applications.Caches;

internal class CleanProductsCacheService : ICleanProductsCacheService
{
    private readonly ProductsMemoryCache _productMemoryCache;

    private readonly ProductsListMemoryCache _productsListMemoryCache;

    private readonly ProductsCountMemoryCache _productsCountMemoryCache;

    public CleanProductsCacheService(
        ProductsMemoryCache productMemoryCache,
        ProductsListMemoryCache productsListMemoryCache,
        ProductsCountMemoryCache productsCountMemoryCache)
    {
        _productMemoryCache = productMemoryCache;
        _productsListMemoryCache = productsListMemoryCache;
        _productsCountMemoryCache = productsCountMemoryCache;
    }

    public void ClearAllCaches()
    {
        _productMemoryCache.Clear();
        _productsListMemoryCache.Clear();
        _productsCountMemoryCache.Clear();
    }

    public void ClearListCaches()
    {
        _productsListMemoryCache.Clear();
        _productsCountMemoryCache.Clear();
    }
}