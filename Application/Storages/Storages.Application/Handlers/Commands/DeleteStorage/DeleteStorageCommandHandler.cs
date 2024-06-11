using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Application.Exceptions;
using Core.Auth.Application.Abstractions.Service;
using Core.Auth.Application.Exceptions;
using Core.Products.Domain;
using Core.Users.Domain.Enums;
using MediatR;
using Storages.Application.Caches;

namespace Storages.Application.Handlers.Commands.DeleteStorage;

internal class DeleteStorageCommandHandler : IRequestHandler<DeleteStorageCommand, Unit>
{
    private readonly IBaseWriteRepository<Storage> _storages;

    private readonly ICurrentUserService _currentUserService;

    private readonly ICleanStoragesCacheService _cleanStoragesCacheService;

    public DeleteStorageCommandHandler(IBaseWriteRepository<Storage> storages,
        ICurrentUserService currentUserService, ICleanStoragesCacheService cleanStoragesCacheService)
    {
        _storages = storages;
        _currentUserService = currentUserService;
        _cleanStoragesCacheService = cleanStoragesCacheService;
    }

    public async Task<Unit> Handle(DeleteStorageCommand request, CancellationToken cancellationToken)
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

        await _storages.RemoveAsync(storage, cancellationToken);
        _cleanStoragesCacheService.ClearAllCaches();
        return default;
    }
}