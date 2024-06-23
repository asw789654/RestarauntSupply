using Core.Application.Abstractions;

namespace Orders.Application.Caches;

public interface IOrdersCountMemoryCache : IBaseCache<int>
{
}
