using FluentValidation;
using Products.Application.Handlers.Queries;

namespace Products.Application.Handlers.Queries.GetProductsCount;

internal class GetProductsCountQueryValidator : AbstractValidator<GetProductsCountQuery>
{
    public GetProductsCountQueryValidator()
    {
        RuleFor(e => e).IsValidListProductFilter();
    }
}