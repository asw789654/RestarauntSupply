using Core.Application.ValidatorsExtensions;
using FluentValidation;

namespace Products.Application.Handlers.Queries.CheckProductsSpoilTime;

internal class CheckProductsSpoilTimeQueryValidator : AbstractValidator<CheckProductsSpoilTimeQuery>
{
    public CheckProductsSpoilTimeQueryValidator()
    {

    }
}