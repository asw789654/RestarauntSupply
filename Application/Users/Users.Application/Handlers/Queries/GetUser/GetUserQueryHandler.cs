using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.BaseRealizations;
using Core.Application.Exceptions;
using Core.Auth.Application.Abstractions.Service;
using Core.Auth.Application.Exceptions;
using Core.Users.Domain;
using Core.Users.Domain.Enums;
using Users.Application.Caches;
using Users.Application.Dtos;

namespace Users.Application.Handlers.Queries.GetUser;

internal class GetUserQueryHandler : BaseCashedQuery<GetUserQuery, GetUserDto>
{
    private readonly IBaseReadRepository<ApplicationUser> _users;

    private readonly IMapper _mapper;

    private readonly ICurrentUserService _currentUserService;

    public GetUserQueryHandler(
        IBaseReadRepository<ApplicationUser> users, 
        IMapper mapper, 
        ICurrentUserService currentUserService, 
        IApplicationUsersMemoryCache cache) : base(cache)
    {
        _currentUserService = currentUserService;
        _users = users;
        _mapper = mapper;
    }

    public override async Task<GetUserDto> SentQueryAsync(GetUserQuery request, CancellationToken cancellationToken)
    {
        if (!_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
        {
            throw new ForbiddenException();
        }

        var idGuid = Guid.Parse(request.Id);
        var user = await _users.AsAsyncRead().SingleOrDefaultAsync(e => e.ApplicationUserId == idGuid, cancellationToken);
        if (user is null)
        {
            throw new NotFoundException(request);
        }

        return _mapper.Map<GetUserDto>(user);
    }
}