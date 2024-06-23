using Core.Application.ValidatorsExtensions;
using FluentValidation;

namespace Products.Application.Handlers.Commands.UpdateProductMailTime;

internal class UpdateProductMailTimeCommandValidator : AbstractValidator<UpdateProductMailTimeCommand>
{
    public UpdateProductMailTimeCommandValidator()
    {
        RuleFor(e => e.ProductId).NotEmpty().IsGuid();
    }
}