using Core.Application.ValidatorsExtensions;
using FluentValidation;

namespace Mails.Applications.Handlers.Queries.CheckMailsSpoilTime;

internal class CheckMailsSpoilTimeQueryValidator : AbstractValidator<CheckMailsSpoilTimeQuery>
{
    public CheckMailsSpoilTimeQueryValidator()
    {

    }
}