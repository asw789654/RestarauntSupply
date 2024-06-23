using Core.Auth.Application.Attributes;
using MediatR;

namespace Products.Application.Handlers.Commands.DeleteProduct;

[RequestAuthorize]
public class DeleteProductCommand : IRequest<Unit>
{
    public string ProductId { get; init; } = default!;
}