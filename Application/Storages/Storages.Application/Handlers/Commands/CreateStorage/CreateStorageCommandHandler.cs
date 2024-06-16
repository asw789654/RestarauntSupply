using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Auth.Application.Abstractions.Service;
using Core.Storages.Domain;
using MediatR;
using Storages.Application.Caches;
using Storages.Application.DTOs;

namespace Storages.Application.Handlers.Commands.CreateStorage;

internal class CreateStorageCommandHandler : IRequestHandler<CreateStorageCommand, GetStorageDto>
{
    private readonly IBaseWriteRepository<Storage> _storages;

    private readonly ICurrentUserService _currentUserService;

    private readonly IMapper _mapper;

    private readonly ICleanStoragesCacheService _cleanStoragesCacheService;

    public CreateStorageCommandHandler(IBaseWriteRepository<Storage> storages, ICurrentUserService currentUserService, IMapper mapper, ICleanStoragesCacheService cleanStoragesCacheService)
    {
        _storages = storages;
        _currentUserService = currentUserService;
        _mapper = mapper;
        _cleanStoragesCacheService = cleanStoragesCacheService;
    }

    public async Task<GetStorageDto> Handle(CreateStorageCommand request, CancellationToken cancellationToken)
    {
        var storage = new Storage
        {
            Name = request.Name,
            Capacity = request.Capacity,
            StorageTypeId = request.StorageTypeId,
        };

        storage = await _storages.AddAsync(storage, cancellationToken);

        _cleanStoragesCacheService.ClearListCaches();

        return _mapper.Map<GetStorageDto>(storage);
    }
}