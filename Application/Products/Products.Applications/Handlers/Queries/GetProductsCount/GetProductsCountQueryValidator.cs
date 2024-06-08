using FluentValidation;

namespace Products.Applications.Handlers.Queries.GetProductsCount;

internal class GetProductsCountQueryValidator : AbstractValidator<GetProductsCountQuery>
{
    public GetProductsCountQueryValidator()
    {
        RuleFor(e => e).IsValidListProductFilter();
    }
}