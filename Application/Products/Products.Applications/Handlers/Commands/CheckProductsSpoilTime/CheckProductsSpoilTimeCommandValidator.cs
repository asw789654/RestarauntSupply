using Core.Application.ValidatorsExtensions;
using FluentValidation;

namespace Products.Application.Handlers.Commands.CheckProductsSpoilTime;

internal class CheckProductsSpoilTimeCommandValidator : AbstractValidator<CheckProductsSpoilTimeCommand>
{
    public CheckProductsSpoilTimeCommandValidator()
    {

    }
}