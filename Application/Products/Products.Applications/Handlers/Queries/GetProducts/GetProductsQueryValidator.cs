using Core.Application.ValidatorsExtensions;
using FluentValidation;

namespace Products.Applications.Handlers.Queries.GetProducts;

internal class GetProductsQueryValidator : AbstractValidator<GetProductsQuery>
{
    public GetProductsQueryValidator()
    {
        RuleFor(e => e).IsValidPaginationFilter();
        RuleFor(e => e).IsValidListProductFilter();
    }
}