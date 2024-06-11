using Core.Application.Abstractions.Mappings;
using Core.Auth.Application.Attributes;
using MediatR;
using Mails.Applications.DTOs;
using Core.Mails.Domain;

namespace Mails.Applications.Handlers.Commands.UpdateProductDelivered;

[RequestAuthorize]
public class UpdateProductDeliveredCommand : IMapTo<Product>, IRequest<GetProductDto>
{
    public string ProductId { get; set; } = default!;
}