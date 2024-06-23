using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Auth.Application.Abstractions.Service;
using MediatR;
using Core.Products.Domain;
using Products.Application.Caches;
using Products.Application.DTOs;

namespace Products.Application.Handlers.Commands.CreateProduct;

internal class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, GetProductDto>
{
    private readonly IBaseWriteRepository<Product> _products;

    private readonly ICurrentUserService _currentUserService;

    private readonly IMapper _mapper;

    private readonly ICleanProductsCacheService _cleanProductsCacheService;

    public CreateProductCommandHandler(IBaseWriteRepository<Product> products, ICurrentUserService currentUserService, IMapper mapper, ICleanProductsCacheService cleanProductsCacheService)
    {
        _products = products;
        _currentUserService = currentUserService;
        _mapper = mapper;
        _cleanProductsCacheService = cleanProductsCacheService;
    }

    public async Task<GetProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Name,
            SpoilTime = request.SpoilTime,
            Volume = request.Volume
        };

        product = await _products.AddAsync(product, cancellationToken);

        _cleanProductsCacheService.ClearListCaches();

        return _mapper.Map<GetProductDto>(product);
    }
}