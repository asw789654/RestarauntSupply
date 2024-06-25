using FluentValidation;

namespace Storages.Application.Handlers.Queries.GetStorageWithProduct;

internal class GetStorageQueryValidator : AbstractValidator<GetStorageQuery>
{
    public GetStorageQueryValidator()
    {
        RuleFor(e => e.StorageId).GreaterThan(0);
    }
}