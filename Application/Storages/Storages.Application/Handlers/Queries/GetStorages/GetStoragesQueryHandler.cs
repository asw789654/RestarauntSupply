using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.BaseRealizations;
using Core.Application.DTOs;
using Core.Auth.Application.Abstractions.Service;
using Core.Auth.Application.Exceptions;
using Core.Storages.Domain;
using Core.Users.Domain.Enums;
using Storages.Application.Caches;
using Storages.Application.DTOs;

namespace Storages.Application.Handlers.Queries.GetStorages;

public class GetStoragesQueryHandler : BaseCashedForUserQuery<GetStoragesQuery, BaseListDto<GetStorageDto>>
{
    private readonly IBaseReadRepository<Storage> _storages;

    private readonly ICurrentUserService _currentUserService;

    private readonly IMapper _mapper;

    public GetStoragesQueryHandler(
        IBaseReadRepository<Storage> storages, 
        ICurrentUserService currentUserService, 
        IMapper mapper, IStoragesListMemoryCache cache) : base(cache, currentUserService.CurrentUserId!.Value)
    {
        _storages = storages;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public override async Task<BaseListDto<GetStorageDto>> SentQueryAsync(GetStoragesQuery request, CancellationToken cancellationToken)
    {
        if (!_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin) ||
            !_currentUserService.UserInRole(ApplicationUserRolesEnum.Client))
        {
            throw new ForbiddenException();
        }

        var query = _storages.AsQueryable();

        if (request.Offset.HasValue)
        {
            query = query.Skip(request.Offset.Value);
        }

        if (request.Limit.HasValue)
        {
            query = query.Take(request.Limit.Value);
        }

        query = query.Where(ListStorageWhere.WhereForAll(request));

        query = query.OrderBy(e => e.StorageId);

        var items = await _storages.AsAsyncRead().ToArrayAsync(query, cancellationToken);

        var totalCount = await _storages.AsAsyncRead().CountAsync(query, cancellationToken);

        return new BaseListDto<GetStorageDto>
        {
            TotalCount = totalCount,
            Items = _mapper.Map<GetStorageDto[]>(items)
        };
    }
}