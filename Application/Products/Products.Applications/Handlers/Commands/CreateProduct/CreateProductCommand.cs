using Core.Auth.Application.Attributes;
using MediatR;
using Products.Application.DTOs;

namespace Products.Application.Handlers.Commands.CreateProduct;

[RequestAuthorize]
public class CreateProductCommand : IRequest<GetProductDto>
{
    public string Name { get; init; } = default!;

    public int Volume { get; set; }

    public DateTime? SpoilTime { get; set; }

    public int StorageTypeId { get; set; }

    
}