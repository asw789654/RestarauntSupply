using FluentValidation;

namespace Storages.Application.Handlers.Commands.CreateStorage;

internal class CreateStorageCommandValidator : AbstractValidator<CreateStorageCommand>
{
    public CreateStorageCommandValidator()
    {
        RuleFor(e => e.Capacity).GreaterThan(0);
        RuleFor(e => e.StorageTypeId).GreaterThan(0);
    }
}