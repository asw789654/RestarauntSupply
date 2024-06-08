using Core.Application.BaseRealizations;
using Core.Application.DTOs;
using Products.Applications.DTOs;

namespace Products.Applications.Caches;

public class ProductsListMemoryCache : BaseCache<BaseListDto<GetProductDto>>;