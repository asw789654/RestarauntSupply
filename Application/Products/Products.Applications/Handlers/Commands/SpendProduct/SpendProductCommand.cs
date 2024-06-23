using Core.Application.Abstractions.Mappings;
using Core.Auth.Application.Attributes;
using Core.Products.Domain;
using MediatR;
using Products.Application.DTOs;

namespace Products.Application.Handlers.Commands.SpendProduct;

[RequestAuthorize]
public class SpendProductCommand : IMapTo<Product>, IRequest<GetProductDto>
{
    public string ProductId { get; set; } = default!;
    public int Volume { get; set; }
}