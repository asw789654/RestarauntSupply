using Core.Application.BaseRealizations;
using Core.Application.DTOs;
using Orders.Applications.DTOs;

namespace Orders.Applications.Caches;

public class OrdersListMemoryCache : BaseCache<BaseListDto<GetOrderDto>>;