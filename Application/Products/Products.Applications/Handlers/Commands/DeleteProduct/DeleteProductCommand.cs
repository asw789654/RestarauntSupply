using Core.Auth.Application.Attributes;
using MediatR;

namespace Products.Applications.Handlers.Commands.DeleteProduct;

[RequestAuthorize]
public class DeleteProductCommand : IRequest<Unit>
{
    public int ProductId { get; init; }
}