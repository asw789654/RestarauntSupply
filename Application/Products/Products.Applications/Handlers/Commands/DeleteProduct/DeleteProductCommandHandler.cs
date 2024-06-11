using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Application.Exceptions;
using Core.Auth.Application.Abstractions.Service;
using Core.Auth.Application.Exceptions;
using Core.Users.Domain.Enums;
using MediatR;
using Products.Applications.Caches;
using Core.Products.Domain;

namespace Products.Applications.Handlers.Commands.DeleteProduct;

internal class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
{
    private readonly IBaseWriteRepository<Product> _products;

    private readonly ICurrentUserService _currentUserService;

    private readonly ICleanProductsCacheService _cleanProductsCacheService;

    public DeleteProductCommandHandler(IBaseWriteRepository<Product> products,
        ICurrentUserService currentUserService, ICleanProductsCacheService cleanProductsCacheService)
    {
        _products = products;
        _currentUserService = currentUserService;
        _cleanProductsCacheService = cleanProductsCacheService;
    }

    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var productId = Guid.Parse(request.ProductId);
        var product = await _products.AsAsyncRead().SingleOrDefaultAsync(e => e.ProductId == productId, cancellationToken);
        if (product is null)
        {
            throw new NotFoundException(request);
        }

        if (!_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
        {
            throw new ForbiddenException();
        }

        await _products.RemoveAsync(product, cancellationToken);
        _cleanProductsCacheService.ClearAllCaches();
        return default;
    }
}