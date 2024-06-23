using FluentValidation;

namespace Orders.Application.Handlers.Queries;

internal class BaseListOrderFilterValidator : AbstractValidator<ListOrderFilter>
{
    public BaseListOrderFilterValidator()
    {
        RuleFor(e => e.FreeText).MaximumLength(50).When(e => e.FreeText is not null);
    }
}

public static class ListOrderFilterValidatorExtensions
{
    internal static IRuleBuilderOptions<T, ListOrderFilter> IsValidListOrderFilter<T>(this IRuleBuilder<T, ListOrderFilter> ruleBuilder)
    {
        return ruleBuilder
            .SetValidator(new BaseListOrderFilterValidator());
    }
}