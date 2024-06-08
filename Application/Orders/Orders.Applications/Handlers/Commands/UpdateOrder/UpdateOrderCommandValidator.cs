using Core.Application.ValidatorsExtensions;
using FluentValidation;

namespace Orders.Applications.Handlers.Commands.UpdateOrder;

internal class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(e => e.OrderId).NotEmpty().IsGuid();
    }
}