using FluentValidation;

namespace Storages.Application.Handlers.Commands.AddProductToStorage;

internal class AddProductToStorageValidator : AbstractValidator<AddProductToStorageCommand>
{
    public AddProductToStorageValidator()
    {
        RuleFor(e => e.StorageId).GreaterThan(0);
    }
}