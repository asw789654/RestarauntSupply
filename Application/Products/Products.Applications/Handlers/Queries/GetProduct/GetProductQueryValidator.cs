using FluentValidation;

namespace Products.Applications.Handlers.Queries.GetProduct;

internal class GetProductQueryValidator : AbstractValidator<GetProductQuery>
{
    public GetProductQueryValidator()
    {
        RuleFor(e => e.ProductId).GreaterThan(0);
    }
}