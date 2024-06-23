using FluentValidation;

namespace Products.Application.Handlers.Queries.GetProduct;

internal class GetProductQueryValidator : AbstractValidator<GetProductQuery>
{
    public GetProductQueryValidator()
    {
        RuleFor(e => e.ProductId).NotEmpty();
    }
}