using Core.Application.Abstractions.Mappings;
using Core.Auth.Application.Attributes;
using Core.Products.Domain;
using MediatR;
using Products.Applications.DTOs;

namespace Products.Applications.Handlers.Commands.SpendProduct;

[RequestAuthorize]
public class SpendProductCommand : IMapTo<Product>, IRequest<GetProductDto>
{
    public int ProductId { get; set; }
    public int Volume { get; set; }
}