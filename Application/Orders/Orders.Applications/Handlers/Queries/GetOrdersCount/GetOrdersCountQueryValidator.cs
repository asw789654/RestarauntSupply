using FluentValidation;
using Orders.Application.Handlers.Queries;

namespace Orders.Application.Handlers.Queries.GetOrdersCount;

internal class GetOrdersCountQueryValidator : AbstractValidator<GetOrdersCountQuery>
{
    public GetOrdersCountQueryValidator()
    {
        RuleFor(e => e).IsValidListOrderFilter();
    }
}