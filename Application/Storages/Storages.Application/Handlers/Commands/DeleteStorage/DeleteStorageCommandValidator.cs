using FluentValidation;

namespace Storages.Application.Handlers.Commands.DeleteStorage;

internal class DeleteStorageCommandValidator : AbstractValidator<DeleteStorageCommand>
{
    public DeleteStorageCommandValidator()
    {
        RuleFor(e => e.StorageId).GreaterThan(0);
    }
}