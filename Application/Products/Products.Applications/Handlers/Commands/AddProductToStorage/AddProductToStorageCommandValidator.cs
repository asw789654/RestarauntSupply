using Core.Application.ValidatorsExtensions;
using FluentValidation;

namespace Products.Application.Handlers.Commands.AddProductToStorage;

internal class AddProductToStorageCommandValidator : AbstractValidator<AddProductToStorageCommand>
{
    public AddProductToStorageCommandValidator()
    {
        RuleFor(e => e.ProductId).NotEmpty().IsGuid();

        RuleFor(e => e.StorageId).GreaterThan(0);
    }
}