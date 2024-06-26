using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.BaseRealizations;
using Core.Auth.Application.Abstractions.Service;
using Core.Auth.Application.Exceptions;
using Core.Storages.Domain;
using Core.Users.Domain.Enums;
using Storages.Application.Caches;

namespace Storages.Application.Handlers.Queries.GetStoragesCount;

internal class GetStoragesCountQueryHandler : BaseCashedForUserQuery<GetStoragesCountQuery, int>
{
    private readonly IBaseReadRepository<Storage> _storages;

    private readonly ICurrentUserService _currentUserService;

    public GetStoragesCountQueryHandler(
        IBaseReadRepository<Storage> storages, 
        IStoragesCountMemoryCache cache, 
        ICurrentUserService currentUserService) : base(cache, currentUserService.CurrentUserId!.Value)
    {
        _storages = storages;
        _currentUserService = currentUserService;
    }

    public override async Task<int> SentQueryAsync(GetStoragesCountQuery request, CancellationToken cancellationToken)
    {
        if (!_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin) &&
            !_currentUserService.UserInRole(ApplicationUserRolesEnum.Client))
        {
            throw new ForbiddenException();
        }
        return await _storages.AsAsyncRead().CountAsync(ListStorageWhere.WhereForAll(request), cancellationToken);
    }
}