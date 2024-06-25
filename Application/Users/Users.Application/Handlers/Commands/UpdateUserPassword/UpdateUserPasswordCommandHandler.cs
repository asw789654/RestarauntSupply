using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Application.Exceptions;
using Core.Auth.Application.Abstractions.Service;
using Core.Auth.Application.Exceptions;
using Core.Auth.Application.Utils;
using Core.Users.Domain;
using Core.Users.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Users.Application.Handlers.Commands.UpdateUserPassword;

internal class UpdateUserPasswordCommandHandler : IRequestHandler<UpdateUserPasswordCommand>
{
    private readonly IBaseWriteRepository<ApplicationUser> _users;
    
    private readonly ICurrentUserService _currentUserService;
    
    private readonly ILogger<UpdateUserPasswordCommandHandler> _logger;

    public UpdateUserPasswordCommandHandler(
        IBaseWriteRepository<ApplicationUser> users, 
        ICurrentUserService currentUserService,
        ILogger<UpdateUserPasswordCommandHandler> logger)
    {
        _users = users;
        _currentUserService = currentUserService;
        _logger = logger;
    }
    
    public async Task Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
    {

        if (!_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin)||
            !_currentUserService.UserInRole(ApplicationUserRolesEnum.Client))
        {
            throw new ForbiddenException();
        }
        var userId = Guid.Parse(request.UserId);
        var user = await _users.AsAsyncRead()
            .SingleOrDefaultAsync(u => u.ApplicationUserId == userId, cancellationToken);
        
        if (user is null)
        {
            throw new NotFoundException(request);
        }

        var newPasswordHash = PasswordHashUtil.Hash(request.Password);
        user.PasswordHash = newPasswordHash;
        user.UpdatedDate = DateTime.UtcNow;
        await _users.UpdateAsync(user, cancellationToken);
        
        _logger.LogWarning($"User password for {user.ApplicationUserId.ToString()} updated by {_currentUserService.CurrentUserId.ToString()}");
    }
}