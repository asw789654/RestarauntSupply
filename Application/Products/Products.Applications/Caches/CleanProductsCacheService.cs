namespace Products.Application.Caches;

internal class CleanProductsCacheService : ICleanProductsCacheService
{
    private readonly IProductsMemoryCache _productMemoryCache;

    private readonly IProductsListMemoryCache _productsListMemoryCache;

    private readonly IProductsCountMemoryCache _productsCountMemoryCache;

    public CleanProductsCacheService(
        IProductsMemoryCache productMemoryCache,
        IProductsListMemoryCache productsListMemoryCache,
        IProductsCountMemoryCache productsCountMemoryCache)
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