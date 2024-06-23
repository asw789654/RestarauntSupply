using Core.Application.ValidatorsExtensions;
using FluentValidation;

namespace Products.Application.Handlers.Commands.UpdateProductDelivered;

internal class UpdateProductDeliveredCommandValidator : AbstractValidator<UpdateProductDeliveredCommand>
{
    public UpdateProductDeliveredCommandValidator()
    {
        RuleFor(e => e.ProductId).NotEmpty().IsGuid();

    }
}