using FluentValidation;

namespace Mails.Applications.Handlers.Queries.GetProduct;

internal class GetProductQueryValidator : AbstractValidator<GetProductQuery>
{
    public GetProductQueryValidator()
    {
        RuleFor(e => e.ProductId).NotEmpty();
    }
}