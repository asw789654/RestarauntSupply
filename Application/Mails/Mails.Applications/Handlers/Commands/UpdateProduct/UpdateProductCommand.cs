using Core.Application.Abstractions.Mappings;
using Core.Auth.Application.Attributes;
using MediatR;
using Mails.Applications.DTOs;
using Core.Mails.Domain;

namespace Mails.Applications.Handlers.Commands.UpdateProduct;

[RequestAuthorize]
public class UpdateProductCommand : IMapTo<Product>, IRequest<GetProductDto>
{
    public string ProductId { get; set; } = default!;

    public string Name { get; set; } = default!;

    public int Volume { get; set; }

    public DateTime? SpoilTime { get; set; }

    public int StorageTypeId { get; set; }
}