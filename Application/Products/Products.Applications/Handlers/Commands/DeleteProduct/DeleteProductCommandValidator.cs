using FluentValidation;
using Core.Application.ValidatorsExtensions;

namespace Products.Application.Handlers.Commands.DeleteProduct;

internal class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(e => e.ProductId).NotEmpty().IsGuid();
    }
}