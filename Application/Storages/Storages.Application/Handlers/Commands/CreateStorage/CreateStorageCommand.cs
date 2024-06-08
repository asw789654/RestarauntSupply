using Core.Auth.Application.Attributes;
using Core.Products.Domain;
using MediatR;
using Storages.Application.DTOs;

namespace Storages.Application.Handlers.Commands.CreateStorage;

[RequestAuthorize]
public class CreateStorageCommand : IRequest<GetStorageDto>
{
    public string Name { get; set; } = default!;
    public int StorageId { get; set; }

    public int Capacity { get; set; }

    public int StorageTypeId { get; set; }

    public ICollection<Product> Products { get; set; } = default!;

}