using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.BaseRealizations;
using Core.Auth.Application.Abstractions.Service;
using Core.Auth.Application.Exceptions;
using Core.Users.Domain.Enums;
using Orders.Application.Caches;
using Orders.Domain;

namespace Orders.Application.Handlers.Queries.GetOrdersCount;

internal class GetOrdersCountQueryHandler : BaseCashedForUserQuery<GetOrdersCountQuery, int>
{
    private readonly IBaseReadRepository<Order> _orders;

    private readonly ICurrentUserService _currentUserService;

    public GetOrdersCountQueryHandler(
        IBaseReadRepository<Order> orders,
        IOrdersCountMemoryCache cache,
        ICurrentUserService currentUserService) : base(cache, currentUserService.CurrentUserId!.Value)
    {
        _orders = orders;
        _currentUserService = currentUserService;
    }

    public override async Task<int> SentQueryAsync(GetOrdersCountQuery request, CancellationToken cancellationToken)
    {
        if (!_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
        {
            return await _orders.AsAsyncRead().CountAsync(ListOrderWhere.WhereForAll(request), cancellationToken);
        }

        else if (!_currentUserService.UserInRole(ApplicationUserRolesEnum.Client))
        {
            var currentUserId = _currentUserService.CurrentUserId;
            return await _orders.AsAsyncRead().CountAsync(ListOrderWhere.WhereForClient(request, currentUserId!.Value), cancellationToken);
        }

        throw new ForbiddenException();
    }
}