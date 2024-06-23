using FluentValidation;

namespace Orders.Application.Handlers.Commands.CreateOrder;

internal class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {

    }
}