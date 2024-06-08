using FluentValidation;

namespace Products.Applications.Handlers.Commands.DeleteProduct;

internal class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(e => e.ProductId).GreaterThan(0);
    }
}