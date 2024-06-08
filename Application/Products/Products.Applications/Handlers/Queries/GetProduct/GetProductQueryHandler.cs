using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.BaseRealizations;
using Core.Application.Exceptions;
using Core.Auth.Application.Abstractions.Service;
using Core.Auth.Application.Exceptions;
using Core.Products.Domain;
using Core.Users.Domain.Enums;
using Products.Applications.Caches;
using Products.Applications.DTOs;

namespace Products.Applications.Handlers.Queries.GetProduct;

internal class GetProductQueryHandler : BaseCashedForUserQuery<GetProductQuery, GetProductDto>
{
    private readonly IBaseReadRepository<Product> _products;

    private readonly ICurrentUserService _currentUserService;

    private readonly IMapper _mapper;

    public GetProductQueryHandler(IBaseReadRepository<Product> products, ICurrentUserService currentUserService, IMapper mapper,
        ProductsMemoryCache cache) : base(cache, currentUserService.CurrentUserId!.Value)
    {
        _products = products;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public override async Task<GetProductDto> SentQueryAsync(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await _products.AsAsyncRead().SingleOrDefaultAsync(e => e.ProductId == request.ProductId, cancellationToken);
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