using Core.Application.Abstractions.Mappings;
using Orders.Domain;

namespace Orders.Application.DTOs;

public class GetOrderDto : IMapFrom<Order>
{
    public Guid OrderId { get; set; }

    public Guid UserId { get; set; }

}