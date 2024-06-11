using AutoMapper;
using Core.Application.Abstractions;
using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Application.DTOs;
using Core.Auth.Application.Abstractions.Service;
using Core.Mails.Domain;
using MediatR;
using Mails.Applications.Caches;
using Mails.Applications.DTOs;
using System.Text.Json;

namespace Mails.Applications.Handlers.Queries.CheckMailsSpoilTime;

internal class CheckMailsSpoilTimeQueryHandler : IRequestHandler<CheckMailsSpoilTimeQuery, BaseListDto<GetProductDto>>
{
    private readonly IBaseWriteRepository<Product> _mails;

    private readonly IMapper _mapper;

    private readonly ICurrentUserService _currentUserService;

    private readonly ICleanMailsCacheService _cleanMailsCacheService;

    private readonly IMqService _mqService;

    public CheckMailsSpoilTimeQueryHandler(
        IBaseWriteRepository<Product> mails,
        IMapper mapper,
        ICurrentUserService currentUserService,
        ICleanMailsCacheService cleanMailsCacheService,
        IMqService mqService)
    {
        _mails = mails;
        _mapper = mapper;
        _currentUserService = currentUserService;
        _cleanMailsCacheService = cleanMailsCacheService;
        _mqService = mqService;
    }

    public async Task<BaseListDto<GetProductDto>> Handle(CheckMailsSpoilTimeQuery request, CancellationToken cancellationToken)
    {
        var query = _mails.AsQueryable();

        query = query.OrderBy(e => e.ProductId);

        query = query.Where(e => e.SpoilTime.Value < DateTime.UtcNow.AddHours(2));

        var items = await _mails.AsAsyncRead().ToArrayAsync(query, cancellationToken);

        var totalCount = await _mails.AsAsyncRead().CountAsync(query, cancellationToken);

        foreach (var product in query)
        {
            _mqService.SendMessage("ProductSpoiled", JsonSerializer.Serialize(product));
        }

        return new BaseListDto<GetProductDto>
        {
            TotalCount = totalCount,
            Items = _mapper.Map<GetProductDto[]>(items)
        };

    }
}