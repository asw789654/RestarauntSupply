using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.BaseRealizations;
using Core.Auth.Application.Abstractions.Service;
using Core.Mails.Domain;
using Core.Users.Domain.Enums;
using Mails.Applications.Caches;
using Mails.Applications.Handlers.Queries;

namespace Mails.Applications.Handlers.Queries.GetMailsCount;

internal class GetMailsCountQueryHandler : BaseCashedForUserQuery<GetMailsCountQuery, int>
{
    private readonly IBaseReadRepository<Product> _mails;

    private readonly ICurrentUserService _currentUserService;

    public GetMailsCountQueryHandler(IBaseReadRepository<Product> mails, MailsCountMemoryCache cache, ICurrentUserService currentUserService) : base(cache, currentUserService.CurrentUserId!.Value)
    {
        _mails = mails;
        _currentUserService = currentUserService;
    }

    public override async Task<int> SentQueryAsync(GetMailsCountQuery request, CancellationToken cancellationToken)
    {
        return await _mails.AsAsyncRead().CountAsync(ListProductWhere.WhereForAll(request), cancellationToken);
    }
}