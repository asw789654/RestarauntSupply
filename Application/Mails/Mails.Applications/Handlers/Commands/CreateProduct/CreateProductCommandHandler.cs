using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Auth.Application.Abstractions.Service;
using MediatR;
using Mails.Applications.Caches;
using Mails.Applications.DTOs;
using Core.Mails.Domain;

namespace Mails.Applications.Handlers.Commands.CreateProduct;

internal class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, GetProductDto>
{
    private readonly IBaseWriteRepository<Product> _mails;

    private readonly ICurrentUserService _currentUserService;

    private readonly IMapper _mapper;

    private readonly ICleanMailsCacheService _cleanMailsCacheService;

    public CreateProductCommandHandler(IBaseWriteRepository<Product> mails, ICurrentUserService currentUserService, IMapper mapper, ICleanMailsCacheService cleanMailsCacheService)
    {
        _mails = mails;
        _currentUserService = currentUserService;
        _mapper = mapper;
        _cleanMailsCacheService = cleanMailsCacheService;
    }

    public async Task<GetProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Name,
            SpoilTime = request.SpoilTime,
            Volume = request.Volume
        };

        product = await _mails.AddAsync(product, cancellationToken);

        _cleanMailsCacheService.ClearListCaches();

        return _mapper.Map<GetProductDto>(product);
    }
}