using Core.Application.ValidatorsExtensions;
using FluentValidation;
using Orders.Applications.Handlers.Commands.CancelOrder;

namespace Orders.Applications.Handlers.Commands.CancelOrder;

internal class CancelOrderCommandValidator : AbstractValidator<CancelOrderCommand>
{
    public CancelOrderCommandValidator()
    {
        RuleFor(e => e.OrderId).NotEmpty().IsGuid();
    }
}