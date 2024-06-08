using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.BaseRealizations;
using Core.Application.DTOs;
using Core.Auth.Application.Abstractions.Service;
using Orders.Applications.Caches;
using Orders.Applications.DTOs;
using Orders.Domain;

namespace Orders.Applications.Handlers.Queries.GetOrders;

public class GetOrdersQueryHandler : BaseCashedForUserQuery<GetOrdersQuery, BaseListDto<GetOrderDto>>
{
    private readonly IBaseReadRepository<Order> _orders;

    private readonly ICurrentUserService _currentUserService;

    private readonly IMapper _mapper;

    public GetOrdersQueryHandler(IBaseReadRepository<Order> orders, ICurrentUserService currentUserService, IMapper mapper, OrdersListMemoryCache cache) : base(cache, currentUserService.CurrentUserId!.Value)
    {
        _orders = orders;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public override async Task<BaseListDto<GetOrderDto>> SentQueryAsync(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var query = _orders.AsQueryable();

        if (request.Offset.HasValue)
        {
            query = query.Skip(request.Offset.Value);
        }

        if (request.Limit.HasValue)
        {
            query = query.Take(request.Limit.Value);
        }

        query = query.Where(ListOrderWhere.WhereForAll(request));

        query = query.OrderBy(e => e.OrderId);

        var items = await _orders.AsAsyncRead().ToArrayAsync(query, cancellationToken);

        var totalCount = await _orders.AsAsyncRead().CountAsync(query, cancellationToken);

        return new BaseListDto<GetOrderDto>
        {
            TotalCount = totalCount,
            Items = _mapper.Map<GetOrderDto[]>(items)
        };
    }
}