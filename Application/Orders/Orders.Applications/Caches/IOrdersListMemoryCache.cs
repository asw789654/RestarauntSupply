using Core.Application.Abstractions;
using Core.Application.DTOs;
using Orders.Application.DTOs;

namespace Orders.Application.Caches;

public interface IOrdersListMemoryCache : IBaseCache<BaseListDto<GetOrderDto>>
{
}
