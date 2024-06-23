using Core.Application.Abstractions.Mappings;
using Core.Auth.Application.Attributes;
using MediatR;
using Core.Products.Domain;
using Products.Application.DTOs;

namespace Products.Application.Handlers.Commands.UpdateProductMailTime;

[RequestAuthorize]
public class UpdateProductMailTimeCommand : IMapTo<Product>, IRequest<GetProductDto>
{
    public string ProductId { get; set; } = default!;
}