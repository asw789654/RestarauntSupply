using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.BaseRealizations;
using Core.Application.Exceptions;
using Core.Auth.Application.Abstractions.Service;
using Core.Auth.Application.Exceptions;
using Core.Mails.Domain;
using Core.Users.Domain.Enums;
using Mails.Applications.Caches;
using Mails.Applications.DTOs;

namespace Mails.Applications.Handlers.Queries.GetProduct;

internal class GetProductQueryHandler : BaseCashedForUserQuery<GetProductQuery, GetProductDto>
{
    private readonly IBaseReadRepository<Product> _mails;

    private readonly ICurrentUserService _currentUserService;

    private readonly IMapper _mapper;

    public GetProductQueryHandler(IBaseReadRepository<Product> mails, ICurrentUserService currentUserService, IMapper mapper,
        MailsMemoryCache cache) : base(cache, currentUserService.CurrentUserId!.Value)
    {
        _mails = mails;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public override async Task<GetProductDto> SentQueryAsync(GetProductQuery request, CancellationToken cancellationToken)
    {
        var productId = Guid.Parse(request.ProductId);
        var product = await _mails.AsAsyncRead().SingleOrDefaultAsync(e => e.ProductId == productId, cancellationToken);
        if (product is null)
        {
            throw new NotFoundException(request);
        }

        if (!_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
        {
            throw new ForbiddenException();
        }

        return _mapper.Map<GetProductDto>(product);
    }
}