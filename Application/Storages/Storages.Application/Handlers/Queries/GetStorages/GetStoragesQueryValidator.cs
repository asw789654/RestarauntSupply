using Core.Application.ValidatorsExtensions;
using FluentValidation;
using Storages.Application.Handlers.Queries;

namespace Storages.Application.Handlers.Queries.GetStorages;

internal class GetStoragesQueryValidator : AbstractValidator<GetStoragesQuery>
{
    public GetStoragesQueryValidator()
    {
        RuleFor(e => e).IsValidPaginationFilter();
        RuleFor(e => e).IsValidListStorageFilter();
    }
}