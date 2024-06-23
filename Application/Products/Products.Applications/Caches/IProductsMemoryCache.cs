using Core.Application.Abstractions;
using Core.Application.DTOs;
using Products.Application.DTOs;

namespace Products.Application.Caches;

public interface IProductsMemoryCache : IBaseCache<GetProductDto>
{
}
