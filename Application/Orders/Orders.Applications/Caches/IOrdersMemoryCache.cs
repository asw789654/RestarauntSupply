using Core.Application.Abstractions;
using Orders.Application.DTOs;

namespace Orders.Application.Caches;

public interface IOrdersMemoryCache : IBaseCache<GetOrderDto>
{
}
