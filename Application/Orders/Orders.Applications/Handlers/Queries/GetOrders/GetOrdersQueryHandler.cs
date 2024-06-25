using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.BaseRealizations;
using Core.Application.DTOs;
using Core.Auth.Application.Abstractions.Service;
using Core.Auth.Application.Exceptions;
using Core.Users.Domain.Enums;
using Orders.Application.Caches;
using Orders.Application.DTOs;
using Orders.Domain;

namespace Orders.Application.Handlers.Queries.GetOrders;

public class GetOrdersQueryHandler : BaseCashedForUserQuery<GetOrdersQuery, BaseListDto<GetOrderDto>>
{
    private readonly IBaseReadRepository<Order> _orders;

    private readonly ICurrentUserService _currentUserService;

    private readonly IMapper _mapper;

    public GetOrdersQueryHandler(
        IBaseReadRepository<Order> orders,
        ICurrentUserService currentUserService,
        IMapper mapper,
        IOrdersListMemoryCache cache) : base(cache, currentUserService.CurrentUserId!.Value)
    {
        _orders = orders;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public override async Task<BaseListDto<GetOrderDto>> SentQueryAsync(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        if (!_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin) ||
            !_currentUserService.UserInRole(ApplicationUserRolesEnum.Client))
        {
            throw new ForbiddenException();
        }

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