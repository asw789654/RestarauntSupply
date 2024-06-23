using Core.Application.Abstractions;

namespace Products.Application.Caches;

public interface IProductsCountMemoryCache : IBaseCache<int>
{
}
