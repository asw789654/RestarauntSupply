using FluentValidation;

namespace Mails.Applications.Handlers.Queries.GetMailsCount;

internal class GetMailsCountQueryValidator : AbstractValidator<GetMailsCountQuery>
{
    public GetMailsCountQueryValidator()
    {
        RuleFor(e => e).IsValidListProductFilter();
    }
}