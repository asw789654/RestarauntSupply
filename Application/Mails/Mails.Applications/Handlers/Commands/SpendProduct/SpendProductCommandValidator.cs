using Core.Application.ValidatorsExtensions;
using FluentValidation;

namespace Mails.Applications.Handlers.Commands.SpendProduct;

internal class SpendProductCommandValidator : AbstractValidator<SpendProductCommand>
{
    public SpendProductCommandValidator()
    {
        RuleFor(e => e.ProductId).NotEmpty().IsGuid();
        RuleFor(e => e.Volume).GreaterThan(0);
    }
}