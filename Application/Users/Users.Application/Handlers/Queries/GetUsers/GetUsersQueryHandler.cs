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

namespace Users.Application.Handlers.Queries.GetUsers;

internal class GetUsersQueryHandler : BaseCashedQuery<GetUsersQuery, BaseListDto<GetUserDto>>
{
    private readonly IBaseReadRepository<ApplicationUser> _users;

    private readonly IMapper _mapper;

    private readonly ICurrentUserService _currentUserService;

    public GetUsersQueryHandler(
        IBaseReadRepository<ApplicationUser> users,
        IMapper mapper,
        ICurrentUserService currentUserService,
        IApplicationUsersListMemoryCache cache) : base(cache)
    {
        _users = users;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public override async Task<BaseListDto<GetUserDto>> SentQueryAsync(GetUsersQuery request, CancellationToken cancellationToken)
    {
        if (!_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
        {
            throw new ForbiddenException();
        }

        var query = _users.AsQueryable().Where(ListWhere.Where(request));

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

        var items = _mapper.Map<GetUserDto[]>(entitiesResult);
        return new BaseListDto<GetUserDto>
        {
            Items = items,
            TotalCount = entitiesCount
        };
    }
}