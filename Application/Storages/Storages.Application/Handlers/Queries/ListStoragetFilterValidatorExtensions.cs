using FluentValidation;

namespace Storages.Application.Handlers.Queries;

internal class BaseListStorageFilterValidator : AbstractValidator<ListStorageFilter>
{
    public BaseListStorageFilterValidator()
    {
        RuleFor(e => e.FreeText).MaximumLength(50).When(e => e.FreeText is not null);
    }
}

public static class ListStorageFilterValidatorExtensions
{
    internal static IRuleBuilderOptions<T, ListStorageFilter> IsValidListStorageFilter<T>(this IRuleBuilder<T, ListStorageFilter> ruleBuilder)
    {
        return ruleBuilder
            .SetValidator(new BaseListStorageFilterValidator());
    }
}