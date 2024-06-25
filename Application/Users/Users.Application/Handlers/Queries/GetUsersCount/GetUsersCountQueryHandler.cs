using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.BaseRealizations;
using Core.Auth.Application.Abstractions.Service;
using Core.Auth.Application.Exceptions;
using Core.Users.Domain;
using Core.Users.Domain.Enums;
using Users.Application.Caches;

namespace Users.Application.Handlers.Queries.GetUsersCount;

internal class GetUsersCountQueryHandler : BaseCashedQuery<GetUsersCountQuery, int>
{
    private readonly IBaseReadRepository<ApplicationUser> _users;

    private readonly ICurrentUserService _currentUserService;

    public GetUsersCountQueryHandler(
        IBaseReadRepository<ApplicationUser> users,
        ICurrentUserService currentUserService,
        IApplicationUsersCountMemoryCache cache) : base(cache)
    {
        _currentUserService = currentUserService;
        _users = users;
    }

    public override async Task<int> SentQueryAsync(GetUsersCountQuery request, CancellationToken cancellationToken)
    {
        if (!_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
        {
            throw new ForbiddenException();
        }

        return await _users.AsAsyncRead().CountAsync(ListWhere.Where(request), cancellationToken);
    }
}