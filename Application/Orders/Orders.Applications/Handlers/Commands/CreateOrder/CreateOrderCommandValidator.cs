using FluentValidation;

namespace Orders.Applications.Handlers.Commands.CreateOrder;

internal class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {

    }
}