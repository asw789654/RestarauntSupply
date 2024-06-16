using AutoMapper;
using Core.Application.Abstractions;
using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Application.DTOs;
using Core.Auth.Application.Abstractions.Service;
using Core.Products.Domain;
using MediatR;
using Products.Applications.Caches;
using Products.Applications.DTOs;
using System.Text.Json;

namespace Products.Applications.Handlers.Queries.CheckProductsSpoilTime;

internal class CheckProductsSpoilTimeQueryHandler : IRequestHandler<CheckProductsSpoilTimeQuery, BaseListDto<GetProductDto>>
{
    private readonly IBaseWriteRepository<Product> _products;

    private readonly IMapper _mapper;

    private readonly ICurrentUserService _currentUserService;

    private readonly ICleanProductsCacheService _cleanProductsCacheService;

    private readonly IMqService _mqService;

    public CheckProductsSpoilTimeQueryHandler(
        IBaseWriteRepository<Product> products,
        IMapper mapper,
        ICurrentUserService currentUserService,
        ICleanProductsCacheService cleanProductsCacheService,
        IMqService mqService)
    {
        _products = products;
        _mapper = mapper;
        _currentUserService = currentUserService;
        _cleanProductsCacheService = cleanProductsCacheService;
        _mqService = mqService;
    }

    public async Task<BaseListDto<GetProductDto>> Handle(CheckProductsSpoilTimeQuery request, CancellationToken cancellationToken)
    {
        var query = _products.AsQueryable();

        query = query.OrderBy(e => e.ProductId);

        query = query.Where(e => e.SpoilTime.Value < DateTime.UtcNow.AddHours(2));

        var items = await _products.AsAsyncRead().ToArrayAsync(query, cancellationToken);

        var totalCount = await _products.AsAsyncRead().CountAsync(query, cancellationToken);

        foreach (var product in query)
        {
            if (!product.MailTime.HasValue || product.MailTime.Value.AddDays(1) <= DateTime.UtcNow) 
            {
                _mqService.SendMessage("spoiledProduct", JsonSerializer.Serialize(product));
                product.MailTime = DateTime.UtcNow;
                
            }
        }
        
        return new BaseListDto<GetProductDto>
        {
            TotalCount = totalCount,
            Items = _mapper.Map<GetProductDto[]>(items)
        };

    }
}