using Core.Application.ValidatorsExtensions;
using FluentValidation;

namespace Mails.Applications.Handlers.Queries.GetMails;

internal class GetMailsQueryValidator : AbstractValidator<GetMailsQuery>
{
    public GetMailsQueryValidator()
    {
        RuleFor(e => e).IsValidPaginationFilter();
        RuleFor(e => e).IsValidListProductFilter();
    }
}