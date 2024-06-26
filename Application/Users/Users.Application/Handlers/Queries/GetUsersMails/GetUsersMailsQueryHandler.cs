using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.BaseRealizations;
using Core.Application.DTOs;
using Core.Auth.Application.Abstractions.Service;
using Core.Auth.Application.Exceptions;
using Core.Users.Domain;
using Core.Users.Domain.Enums;
using Users.Application.Caches;
using Users.Application.Dtos;

namespace Users.Application.Handlers.Queries.GetUsersMails;

internal class GetUsersMailsQueryHandler : BaseCashedQuery<GetUsersMailsQuery, BaseListDto<GetUserMailDto>>
{
    private readonly IBaseReadRepository<ApplicationUser> _users;

    private readonly IMapper _mapper;

    private readonly ICurrentUserService _currentUserService;

    public GetUsersMailsQueryHandler(
        IBaseReadRepository<ApplicationUser> users, 
        IMapper mapper, 
        ICurrentUserService currentUserService,
        IApplicationUsersMailListMemoryCache cache) : base(cache)
    {
        _currentUserService = currentUserService;
        _users = users;
        _mapper = mapper;
    }

    public override async Task<BaseListDto<GetUserMailDto>> SentQueryAsync(GetUsersMailsQuery request, CancellationToken cancellationToken)
    {
        if (!_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
        {
            throw new ForbiddenException();
        }

        var query = _users.AsQueryable().Where(ListUserWhere.Where(request));

        if (request.Offset.HasValue)
        {
            query = query.Skip(request.Offset.Value);
        }

        if (request.Limit.HasValue)
        {
            query = query.Take(request.Limit.Value);
        }

        query = query.OrderBy(e => e.ApplicationUserId);

        var entitiesResult = await _users.AsAsyncRead().ToArrayAsync(query, cancellationToken);
        var entitiesCount = await _users.AsAsyncRead().CountAsync(query, cancellationToken);

        var items = _mapper.Map<GetUserMailDto[]>(entitiesResult);
        return new BaseListDto<GetUserMailDto>
        {
            Items = items,
            TotalCount = entitiesCount
        };
    }
}