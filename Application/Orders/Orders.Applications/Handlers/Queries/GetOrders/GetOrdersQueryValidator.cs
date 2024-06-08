using Core.Application.ValidatorsExtensions;
using FluentValidation;

namespace Orders.Applications.Handlers.Queries.GetOrders;

internal class GetOrdersQueryValidator : AbstractValidator<GetOrdersQuery>
{
    public GetOrdersQueryValidator()
    {
        RuleFor(e => e).IsValidPaginationFilter();
        RuleFor(e => e).IsValidListOrderFilter();
    }
}