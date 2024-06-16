using Core.Application.Abstractions.Mappings;
using Core.Auth.Application.Attributes;
using MediatR;
using Products.Applications.DTOs;
using Core.Products.Domain;

namespace Products.Applications.Handlers.Commands.UpdateProductMailTime;

[RequestAuthorize]
public class UpdateProductMailTimeCommand : IMapTo<Product>, IRequest<GetProductDto>
{
    public string ProductId { get; set; } = default!;
}