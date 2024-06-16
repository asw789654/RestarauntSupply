using Core.Application.Abstractions.Mappings;
using Core.Auth.Application.Attributes;
using Core.Products.Domain;
using Core.Storages.Domain;
using MediatR;
using Storages.Application.DTOs;

namespace Storages.Application.Handlers.Commands.UpdateStorage;

[RequestAuthorize]
public class UpdateStorageCommand : IMapTo<Storage>, IRequest<GetStorageDto>
{
    public string Name { get; set; } = default!;
    public int StorageId { get; set; }

    public int Capacity { get; set; }

    public int StorageTypeId { get; set; }

}