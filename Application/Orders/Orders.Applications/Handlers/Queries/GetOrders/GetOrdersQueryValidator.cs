using Core.Application.ValidatorsExtensions;
using FluentValidation;
using Orders.Application.Handlers.Queries;

namespace Orders.Application.Handlers.Queries.GetOrders;

internal class GetOrdersQueryValidator : AbstractValidator<GetOrdersQuery>
{
    public GetOrdersQueryValidator()
    {
        RuleFor(e => e).IsValidPaginationFilter();
        RuleFor(e => e).IsValidListOrderFilter();
    }
}