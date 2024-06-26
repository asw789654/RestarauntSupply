using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Application.Exceptions;
using Core.Auth.Application.Abstractions.Service;
using Core.Auth.Application.Exceptions;
using Core.Users.Domain.Enums;
using MediatR;
using Core.Products.Domain;
using Products.Application.Caches;
using Products.Application.DTOs;

namespace Products.Application.Handlers.Commands.UpdateProductDelivered;

internal class UpdateProductDeliveredCommandHandler : IRequestHandler<UpdateProductDeliveredCommand, GetProductDto>
{
    private readonly IBaseWriteRepository<Product> _products;

    private readonly IMapper _mapper;

    private readonly ICurrentUserService _currentUserService;

    private readonly ICleanProductsCacheService _cleanProductsCacheService;

    public UpdateProductDeliveredCommandHandler(IBaseWriteRepository<Product> products, IMapper mapper, ICurrentUserService currentUserService, ICleanProductsCacheService cleanProductsCacheService)
    {
        _products = products;
        _mapper = mapper;
        _currentUserService = currentUserService;
        _cleanProductsCacheService = cleanProductsCacheService;
    }

    public async Task<GetProductDto> Handle(UpdateProductDeliveredCommand request, CancellationToken cancellationToken)
    {
        if (!_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
        {
            throw new ForbiddenException();
        }

        var productId = Guid.Parse(request.ProductId);
        var product = await _products.AsAsyncRead().SingleOrDefaultAsync(e => e.ProductId == productId, cancellationToken);
        if (product is null)
        {
            throw new NotFoundException(request);
        }

        _mapper.Map(request, product);
        product.IsDelivered = true;
        product = await _products.UpdateAsync(product, cancellationToken);
        _cleanProductsCacheService.ClearAllCaches();
        return _mapper.Map<GetProductDto>(product);
    }
}