using Core.Application.Abstractions.Mappings;
using Core.Auth.Application.Attributes;
using MediatR;
using Core.Products.Domain;
using Products.Application.DTOs;

namespace Products.Application.Handlers.Commands.AddProductToStorage;

[RequestAuthorize]
public class AddProductToStorageCommand : IMapTo<Product>, IRequest<GetProductDto>
{
    public string ProductId { get; set; } = default!;

    public int StorageId { get; set; } = default!;
}