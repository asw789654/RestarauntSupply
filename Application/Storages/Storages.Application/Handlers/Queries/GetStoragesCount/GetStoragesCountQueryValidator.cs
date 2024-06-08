using FluentValidation;

namespace Storages.Application.Handlers.Queries.GetStoragesCount;

internal class GetStoragesCountQueryValidator : AbstractValidator<GetStoragesCountQuery>
{
    public GetStoragesCountQueryValidator()
    {
        RuleFor(e => e).IsValidListStorageFilter();
    }
}