using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.BaseRealizations;
using Core.Application.Exceptions;
using Core.Auth.Application.Abstractions.Service;
using Core.Auth.Application.Exceptions;
using Core.Users.Domain.Enums;
using Orders.Application.Caches;
using Orders.Application.DTOs;
using Orders.Domain;

namespace Orders.Application.Handlers.Queries.GetOrder;

internal class GetOrderQueryHandler : BaseCashedForUserQuery<GetOrderQuery, GetOrderDto>
{
    private readonly IBaseReadRepository<Order> _orders;

    private readonly ICurrentUserService _currentUserService;

    private readonly IMapper _mapper;

    public GetOrderQueryHandler(
        IBaseReadRepository<Order> orders,
        ICurrentUserService currentUserService,
        IMapper mapper,
        IOrdersMemoryCache cache) : base(cache, currentUserService.CurrentUserId!.Value)
    {
        _orders = orders;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public override async Task<GetOrderDto> SentQueryAsync(GetOrderQuery request, CancellationToken cancellationToken)
    {
        if (!_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin) ||
            !_currentUserService.UserInRole(ApplicationUserRolesEnum.Client))
        {
            throw new ForbiddenException();
        }

        var orderId = Guid.Parse(request.OrderId);
        var order = await _orders.AsAsyncRead().SingleOrDefaultAsync(e => e.OrderId == orderId, cancellationToken);
        if (order is null)
        {
            throw new NotFoundException(request);
        }

        return _mapper.Map<GetOrderDto>(order);
    }
}