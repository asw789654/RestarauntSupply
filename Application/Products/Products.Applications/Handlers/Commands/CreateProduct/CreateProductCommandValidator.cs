using FluentValidation;

namespace Products.Applications.Handlers.Commands.CreateProduct;

internal class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(200);
    }
}