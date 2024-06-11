using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Application.Exceptions;
using Core.Auth.Application.Abstractions.Service;
using Core.Auth.Application.Exceptions;
using Core.Users.Domain.Enums;
using MediatR;
using Mails.Applications.Caches;
using Core.Mails.Domain;

namespace Mails.Applications.Handlers.Commands.DeleteProduct;

internal class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
{
    private readonly IBaseWriteRepository<Product> _mails;

    private readonly ICurrentUserService _currentUserService;

    private readonly ICleanMailsCacheService _cleanMailsCacheService;

    public DeleteProductCommandHandler(IBaseWriteRepository<Product> mails,
        ICurrentUserService currentUserService, ICleanMailsCacheService cleanMailsCacheService)
    {
        _mails = mails;
        _currentUserService = currentUserService;
        _cleanMailsCacheService = cleanMailsCacheService;
    }

    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
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

        await _mails.RemoveAsync(product, cancellationToken);
        _cleanMailsCacheService.ClearAllCaches();
        return default;
    }
}