using Core.Application.ValidatorsExtensions;
using FluentValidation;

namespace Orders.Application.Handlers.Commands.CancelOrder;

internal class CancelOrderCommandValidator : AbstractValidator<CancelOrderCommand>
{
    public CancelOrderCommandValidator()
    {
        RuleFor(e => e.OrderId).NotEmpty().IsGuid();
    }
}