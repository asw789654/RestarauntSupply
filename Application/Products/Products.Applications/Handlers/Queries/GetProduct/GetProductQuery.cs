using Core.Auth.Application.Attributes;
using MediatR;

namespace Products.Applications.Handlers.Queries.GetProduct;

[RequestAuthorize]
public class GetProductQuery : IRequest<DTOs.GetProductDto>
{
    public string ProductId { get; init; } = default!;
}