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

namespace Orders.Application.Handlers.Commands.UpdateOrderStatus;

internal class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand, GetOrderDto>
{
    private readonly IBaseWriteRepository<Order> _orders;

    private readonly IMapper _mapper;

    private readonly ICurrentUserService _currentUserService;

    private readonly ICleanOrdersCacheService _cleanOrdersCacheService;

    private readonly IMqService _mqService;

    public UpdateOrderStatusCommandHandler(
        IBaseWriteRepository<Order> orders,
        IMapper mapper,
        ICurrentUserService currentUserService,
        ICleanOrdersCacheService cleanOrdersCacheService,
        IMqService mqService)
    {
        _orders = orders;
        _mapper = mapper;
        _currentUserService = currentUserService;
        _cleanOrdersCacheService = cleanOrdersCacheService;
        _mqService = mqService;
    }

    public async Task<GetOrderDto> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
    {
        if (!_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
        {
            throw new ForbiddenException();
        }

        var orderId = Guid.Parse(request.OrderId);
        var order = await _orders.AsAsyncRead().SingleOrDefaultAsync(e => e.OrderId == orderId, cancellationToken);
        if (order is null)
        {
            throw new NotFoundException(request);
        }

        _mapper.Map(request, order);

        if (order.OrderStatusId == 3)
        {
            _mqService.SendMessage("addProductOnOrderComplete", JsonSerializer.Serialize(request.OrderId));
        }

        order = await _orders.UpdateAsync(order, cancellationToken);
        _cleanOrdersCacheService.ClearAllCaches();
        return _mapper.Map<GetOrderDto>(order);
    }
}