using Core.Application.Abstractions.Mappings;
using Core.Auth.Application.Attributes;
using Core.Storages.Domain;
using MediatR;
using Storages.Application.DTOs;

namespace Storages.Application.Handlers.Commands.AddProductToStorage;

[RequestAuthorize]
public class AddProductToStorageCommand : IMapTo<Storage>, IRequest<GetStorageDto>
{
    public int StorageId { get; set; }

    public string ProductId { get; set; } = default!;

}