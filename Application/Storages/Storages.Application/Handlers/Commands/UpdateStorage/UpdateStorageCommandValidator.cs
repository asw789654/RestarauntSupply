using FluentValidation;

namespace Storages.Application.Handlers.Commands.UpdateStorage;

internal class UpdateStorageCommandValidator : AbstractValidator<UpdateStorageCommand>
{
    public UpdateStorageCommandValidator()
    {
        RuleFor(e => e.StorageId).GreaterThan(0);
        RuleFor(e => e.Capacity).GreaterThan(0);

    }
}