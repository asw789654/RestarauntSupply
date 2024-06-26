using AutoMapper;
using Core.Application.Abstractions;
using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Application.Exceptions;
using Core.Auth.Application.Abstractions.Service;
using Core.Auth.Application.Exceptions;
using Core.Users.Domain.Enums;
using MediatR;
using Orders.Application.Caches;
using Orders.Application.DTOs;
using Orders.Domain;
using System.Text.Json;

namespace Orders.Application.Handlers.Commands.CancelOrder;

internal class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, GetOrderDto>
{
    private readonly IBaseWriteRepository<Order> _orders;

    private readonly IMapper _mapper;

    private readonly ICurrentUserService _currentUserService;

    private readonly ICleanOrdersCacheService _cleanOrdersCacheService;

    public CancelOrderCommandHandler(
        IBaseWriteRepository<Order> orders,
        IMapper mapper,
        ICurrentUserService currentUserService,
        ICleanOrdersCacheService cleanOrdersCacheService)
    {
        _orders = orders;
        _mapper = mapper;
        _currentUserService = currentUserService;
        _cleanOrdersCacheService = cleanOrdersCacheService;
    }

    public async Task<GetOrderDto> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {

        if (!_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin) 
            && !_currentUserService.UserInRole(ApplicationUserRolesEnum.Client))
        {
            throw new ForbiddenException();
        }

        var orderId = Guid.Parse(request.OrderId);
        var order = await _orders.AsAsyncRead().SingleOrDefaultAsync(e => e.OrderId == orderId, cancellationToken);

        if (order is null)
        {
            throw new NotFoundException(request);
        }

        
        order.OrderStatusId = 4;

        _mapper.Map(request, order);

        order = await _orders.UpdateAsync(order, cancellationToken);
        _cleanOrdersCacheService.ClearAllCaches();
        return _mapper.Map<GetOrderDto>(order);
    }
}