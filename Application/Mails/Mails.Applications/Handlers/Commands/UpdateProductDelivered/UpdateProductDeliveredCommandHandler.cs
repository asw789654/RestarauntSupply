using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Application.Exceptions;
using Core.Auth.Application.Abstractions.Service;
using Core.Auth.Application.Exceptions;
using Core.Users.Domain.Enums;
using MediatR;
using Mails.Applications.Caches;
using Mails.Applications.DTOs;
using Core.Mails.Domain;

namespace Mails.Applications.Handlers.Commands.UpdateProductDelivered;

internal class UpdateProductDeliveredCommandHandler : IRequestHandler<UpdateProductDeliveredCommand, GetProductDto>
{
    private readonly IBaseWriteRepository<Product> _mails;

    private readonly IMapper _mapper;

    private readonly ICurrentUserService _currentUserService;

    private readonly ICleanMailsCacheService _cleanMailsCacheService;

    public UpdateProductDeliveredCommandHandler(IBaseWriteRepository<Product> mails, IMapper mapper, ICurrentUserService currentUserService, ICleanMailsCacheService cleanMailsCacheService)
    {
        _mails = mails;
        _mapper = mapper;
        _currentUserService = currentUserService;
        _cleanMailsCacheService = cleanMailsCacheService;
    }

    public async Task<GetProductDto> Handle(UpdateProductDeliveredCommand request, CancellationToken cancellationToken)
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

        _mapper.Map(request, product);
        product.IsDelivered = true;
        product = await _mails.UpdateAsync(product, cancellationToken);
        _cleanMailsCacheService.ClearAllCaches();
        return _mapper.Map<GetProductDto>(product);
    }
}