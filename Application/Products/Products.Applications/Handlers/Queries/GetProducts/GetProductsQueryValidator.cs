using Core.Application.ValidatorsExtensions;
using FluentValidation;
using Products.Application.Handlers.Queries;

namespace Products.Application.Handlers.Queries.GetProducts;

internal class GetProductsQueryValidator : AbstractValidator<GetProductsQuery>
{
    public GetProductsQueryValidator()
    {
        RuleFor(e => e).IsValidPaginationFilter();
        RuleFor(e => e).IsValidListProductFilter();
    }
}