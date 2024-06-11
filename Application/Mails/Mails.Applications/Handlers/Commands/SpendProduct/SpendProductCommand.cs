using Core.Application.Abstractions.Mappings;
using Core.Auth.Application.Attributes;
using Core.Mails.Domain;
using MediatR;
using Mails.Applications.DTOs;

namespace Mails.Applications.Handlers.Commands.SpendProduct;

[RequestAuthorize]
public class SpendProductCommand : IMapTo<Product>, IRequest<GetProductDto>
{
    public string ProductId { get; set; } = default!;
    public int Volume { get; set; }
}