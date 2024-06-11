using Core.Application.Abstractions.Mappings;
using Core.Auth.Application.Attributes;
using MediatR;
using Products.Applications.DTOs;
using Core.Products.Domain;

namespace Products.Applications.Handlers.Commands.UpdateProduct;

[RequestAuthorize]
public class UpdateProductCommand : IMapTo<Product>, IRequest<GetProductDto>
{
    public string ProductId { get; set; } = default!;

    public string Name { get; set; } = default!;

    public int Volume { get; set; }

    public DateTime? SpoilTime { get; set; }

    public int StorageTypeId { get; set; }
}