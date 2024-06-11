using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.BaseRealizations;
using Core.Application.DTOs;
using Core.Auth.Application.Abstractions.Service;
using Core.Mails.Domain;
using Mails.Applications.Caches;
using Mails.Applications.DTOs;

namespace Mails.Applications.Handlers.Queries.GetMails;

public class GetMailsQueryHandler : BaseCashedForUserQuery<GetMailsQuery, BaseListDto<GetProductDto>>
{
    private readonly IBaseReadRepository<Product> _mails;

    private readonly ICurrentUserService _currentUserService;

    private readonly IMapper _mapper;

    public GetMailsQueryHandler(IBaseReadRepository<Product> mails, ICurrentUserService currentUserService, IMapper mapper, MailsListMemoryCache cache) : base(cache, currentUserService.CurrentUserId!.Value)
    {
        _mails = mails;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public override async Task<BaseListDto<GetProductDto>> SentQueryAsync(GetMailsQuery request, CancellationToken cancellationToken)
    {
        var query = _mails.AsQueryable();

        if (request.Offset.HasValue)
        {
            query = query.Skip(request.Offset.Value);
        }

        if (request.Limit.HasValue)
        {
            query = query.Take(request.Limit.Value);
        }

        query = query.Where(ListProductWhere.WhereForAll(request));

        query = query.OrderBy(e => e.ProductId);

        var items = await _mails.AsAsyncRead().ToArrayAsync(query, cancellationToken);

        var totalCount = await _mails.AsAsyncRead().CountAsync(query, cancellationToken);

        return new BaseListDto<GetProductDto>
        {
            TotalCount = totalCount,
            Items = _mapper.Map<GetProductDto[]>(items)
        };
    }
}