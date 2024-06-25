using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Application.Exceptions;
using Core.Auth.Application.Abstractions.Service;
using Core.Auth.Application.Exceptions;
using Core.Storages.Domain;
using Core.Users.Domain.Enums;
using MediatR;
using Storages.Application.Caches;
using Storages.Application.DTOs;

namespace Storages.Application.Handlers.Commands.UpdateStorage;

internal class UpdateStorageCommandHandler : IRequestHandler<UpdateStorageCommand, GetStorageDto>
{
    private readonly IBaseWriteRepository<Storage> _storages;

    private readonly IMapper _mapper;

    private readonly ICurrentUserService _currentUserService;

    private readonly ICleanStoragesCacheService _cleanStoragesCacheService;

    public UpdateStorageCommandHandler(IBaseWriteRepository<Storage> storages, IMapper mapper, ICurrentUserService currentUserService, ICleanStoragesCacheService cleanStoragesCacheService)
    {
        _storages = storages;
        _mapper = mapper;
        _currentUserService = currentUserService;
        _cleanStoragesCacheService = cleanStoragesCacheService;
    }

    public async Task<GetStorageDto> Handle(UpdateStorageCommand request, CancellationToken cancellationToken)
    {

        if (!_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
        {
            throw new ForbiddenException();
        }

        var storage = await _storages.AsAsyncRead().SingleOrDefaultAsync(e => e.StorageId == request.StorageId, cancellationToken);
        if (storage is null)
        {
            throw new NotFoundException(request);
        }

        _mapper.Map(request, storage);
        storage = await _storages.UpdateAsync(storage, cancellationToken);
        _cleanStoragesCacheService.ClearAllCaches();
        return _mapper.Map<GetStorageDto>(storage);
    }
}