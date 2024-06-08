using FluentValidation;

namespace Products.Applications.Handlers.Commands.UpdateProduct;

internal class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(e => e.ProductId).GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(200);
    }
}