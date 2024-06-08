using FluentValidation;
using Orders.Applications.Handlers.Queries;

namespace Orders.Applications.Handlers.Queries.GetOrdersCount;

internal class GetOrdersCountQueryValidator : AbstractValidator<GetOrdersCountQuery>
{
    public GetOrdersCountQueryValidator()
    {
        RuleFor(e => e).IsValidListOrderFilter();
    }
}