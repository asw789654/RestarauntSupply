using AutoMapper;
using Core.Application.Abstractions.Persistence.Repository.Writing;
using Core.Auth.Application.Abstractions.Service;
using Core.Auth.Application.Exceptions;
using Core.Products.Domain;
using Core.Users.Domain.Enums;
using MediatR;
using Orders.Applications.Caches;
using Orders.Applications.DTOs;
using Orders.Domain;

namespace Orders.Applications.Handlers.Commands.CreateOrder;

internal class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, GetOrderDto>
{
    private readonly IBaseWriteRepository<Order> _orders;

    private readonly ICurrentUserService _currentUserService;

    private readonly IMapper _mapper;

    private readonly ICleanOrdersCacheService _cleanOrdersCacheService;

    private readonly IBaseWriteRepository<Product> _products;

    public CreateOrderCommandHandler(
        IBaseWriteRepository<Order> orders, 
        ICurrentUserService currentUserService, 
        IMapper mapper, 
        ICleanOrdersCacheService cleanOrdersCacheService,
        IBaseWriteRepository<Product> products)
    {
        _orders = orders;
        _currentUserService = currentUserService;
        _mapper = mapper;
        _cleanOrdersCacheService = cleanOrdersCacheService;
        _products = products;
    }

    public async Task<GetOrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.CurrentUserId;

        if (_currentUserService.CurrentUserId != userId &&
            !_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
        {
            throw new ForbiddenException();
        }
        /*
        var products = request.Products;

        foreach (var product in request.Products)
        {          
            await _products.UpdateAsync(product, cancellationToken);
        }
        */
        var order = new Order
        {
            UserId = userId,
            Products = request.Products
        };

        order = await _orders.AddAsync(order, cancellationToken);

        _cleanOrdersCacheService.ClearListCaches();

        return _mapper.Map<GetOrderDto>(order);
    }
}