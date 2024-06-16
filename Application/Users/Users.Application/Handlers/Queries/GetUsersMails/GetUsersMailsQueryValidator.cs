using Core.Application.ValidatorsExtensions;
using FluentValidation;

namespace Users.Application.Handlers.Queries.GetUsersMails;

internal class GetUsersMailsQueryValidator : AbstractValidator<GetUsersMailsQuery>
{
    public GetUsersMailsQueryValidator()
    {
        RuleFor(e => e).IsValidListUserFilter();
        RuleFor(e => e).IsValidPaginationFilter();
    }
    
}