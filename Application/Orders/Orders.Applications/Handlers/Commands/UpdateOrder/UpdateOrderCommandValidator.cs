using Core.Application.ValidatorsExtensions;
using FluentValidation;

namespace Orders.Application.Handlers.Commands.UpdateOrder;

internal class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(e => e.OrderId).NotEmpty().IsGuid();
    }
}