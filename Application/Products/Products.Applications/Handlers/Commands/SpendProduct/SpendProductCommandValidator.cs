using FluentValidation;

namespace Products.Applications.Handlers.Commands.SpendProduct;

internal class SpendProductCommandValidator : AbstractValidator<SpendProductCommand>
{
    public SpendProductCommandValidator()
    {
        RuleFor(e => e.ProductId).GreaterThan(0);
        RuleFor(e => e.Volume).GreaterThan(0);
    }
}