using Core.Products.Domain;
using Core.Users.Domain;

namespace Orders.Domain;

public class Order
{
    public Guid OrderId { get; set; }

    public Guid UserId { get; set; }

    public int OrderStatusId { get; set; } = 1;

    public ApplicationUser User { get; set; } = default!;

    public List<Product> Products { get; set; } = default!;

    public OrderStatus OrderStatus { get; set; } = default!;
}