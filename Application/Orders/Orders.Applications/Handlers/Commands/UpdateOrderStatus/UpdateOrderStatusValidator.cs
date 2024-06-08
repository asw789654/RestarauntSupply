using Core.Application.ValidatorsExtensions;
using FluentValidation;

namespace Orders.Applications.Handlers.Commands.UpdateOrderStatus;

internal class UpdateOrderStatusCommandValidator : AbstractValidator<UpdateOrderStatusCommand>
{
    public UpdateOrderStatusCommandValidator()
    {
        RuleFor(e => e.OrderId).NotEmpty().IsGuid();
        RuleFor(e => e.OrderStatusId).GreaterThan(0);
    }
}