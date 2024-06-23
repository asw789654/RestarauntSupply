using FluentValidation;

namespace Products.Application.Handlers.Queries;

internal class BaseListProductFilterValidator : AbstractValidator<ListProductFilter>
{
    public BaseListProductFilterValidator()
    {
        RuleFor(e => e.FreeText).MaximumLength(50).When(e => e.FreeText is not null);
    }
}

public static class ListProductFilterValidatorExtensions
{
    internal static IRuleBuilderOptions<T, ListProductFilter> IsValidListProductFilter<T>(this IRuleBuilder<T, ListProductFilter> ruleBuilder)
    {
        return ruleBuilder
            .SetValidator(new BaseListProductFilterValidator());
    }
}