using Core.Application.ValidatorsExtensions;
using FluentValidation;

namespace Mails.Applications.Handlers.Commands.UpdateProduct;

internal class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(e => e.ProductId).NotEmpty().IsGuid();
        RuleFor(e => e.Name).NotEmpty().MaximumLength(200);
    }
}