using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.BaseRealizations;
using Core.Application.Exceptions;
using Core.Auth.Application.Abstractions.Service;
using Core.Auth.Application.Exceptions;
using Core.Storages.Domain;
using Core.Users.Domain.Enums;
using Storages.Application.Caches;
using Storages.Application.DTOs;

namespace Storages.Application.Handlers.Queries.GetStorage;

internal class GetStorageQueryHandler : BaseCashedForUserQuery<GetStorageQuery, GetStorageDto>
{
    private readonly IBaseReadRepository<Storage> _storages;

    private readonly ICurrentUserService _currentUserService;

    private readonly IMapper _mapper;

    public GetStorageQueryHandler(IBaseReadRepository<Storage> storages, ICurrentUserService currentUserService, IMapper mapper,
        IStoragesMemoryCache cache) : base(cache, currentUserService.CurrentUserId!.Value)
    {
        _storages = storages;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public override async Task<GetStorageDto> SentQueryAsync(GetStorageQuery request, CancellationToken cancellationToken)
    {
        var storage = await _storages.AsAsyncRead().SingleOrDefaultAsync(e => e.StorageId == request.StorageId, cancellationToken);
        if (storage is null)
        {
            throw new NotFoundException(request);
        }

        if (!_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
        {
            throw new ForbiddenException();
        }

        return _mapper.Map<GetStorageDto>(storage);
    }
}