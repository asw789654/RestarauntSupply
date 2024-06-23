using Core.Application.ValidatorsExtensions;
using FluentValidation;

namespace Orders.Application.Handlers.Queries.GetOrder;

internal class GetOrderQueryValidator : AbstractValidator<GetOrderQuery>
{
    public GetOrderQueryValidator()
    {
        RuleFor(e => e.OrderId).NotEmpty().IsGuid();
    }
}