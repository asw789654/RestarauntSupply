using Core.Application.ValidatorsExtensions;
using FluentValidation;

namespace Products.Application.Handlers.Commands.SpendProduct;

internal class SpendProductCommandValidator : AbstractValidator<SpendProductCommand>
{
    public SpendProductCommandValidator()
    {
        RuleFor(e => e.ProductId).NotEmpty().IsGuid();
        RuleFor(e => e.Volume).GreaterThan(0);
    }
}