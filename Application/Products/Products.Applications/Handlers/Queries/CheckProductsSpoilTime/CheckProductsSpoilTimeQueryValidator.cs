using Core.Application.ValidatorsExtensions;
using FluentValidation;

namespace Products.Applications.Handlers.Queries.CheckProductsSpoilTime;

internal class CheckProductsSpoilTimeQueryValidator : AbstractValidator<CheckProductsSpoilTimeQuery>
{
    public CheckProductsSpoilTimeQueryValidator()
    {

    }
}