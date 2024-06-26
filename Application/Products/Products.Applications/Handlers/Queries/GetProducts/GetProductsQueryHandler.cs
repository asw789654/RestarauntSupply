using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Read;
using Core.Application.BaseRealizations;
using Core.Application.DTOs;
using Core.Auth.Application.Abstractions.Service;
using Core.Auth.Application.Exceptions;
using Core.Products.Domain;
using Core.Users.Domain.Enums;
using Products.Application.Caches;
using Products.Application.DTOs;

namespace Products.Application.Handlers.Queries.GetProducts;

public class GetProductsQueryHandler : BaseCashedForUserQuery<GetProductsQuery, BaseListDto<GetProductDto>>
{
    private readonly IBaseReadRepository<Product> _products;

    private readonly ICurrentUserService _currentUserService;

    private readonly IMapper _mapper;

    public GetProductsQueryHandler(
        IBaseReadRepository<Product> products,
        ICurrentUserService currentUserService,
        IMapper mapper,
        IProductsListMemoryCache cache) : base(cache, currentUserService.CurrentUserId!.Value)
    {
        _products = products;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public override async Task<BaseListDto<GetProductDto>> SentQueryAsync(GetProductsQuery request, CancellationToken cancellationToken)
    {
        if (!_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin) &&
            !_currentUserService.UserInRole(ApplicationUserRolesEnum.Client))
        {
            throw new ForbiddenException();
        }

        var query = _products.AsQueryable();

        if (_currentUserService.UserInRole(ApplicationUserRolesEnum.Client))
        {
            query = query.Where(ListProductWhere.WhereForClient(request));
        }



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

        var items = await _products.AsAsyncRead().ToArrayAsync(query, cancellationToken);

        var totalCount = await _products.AsAsyncRead().CountAsync(query, cancellationToken);

        return new BaseListDto<GetProductDto>
        {
            TotalCount = totalCount,
            Items = _mapper.Map<GetProductDto[]>(items)
        };
    }
}