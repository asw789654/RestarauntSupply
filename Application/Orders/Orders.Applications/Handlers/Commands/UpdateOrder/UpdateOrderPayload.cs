using Core.Products.Domain;

namespace Orders.Applications.Handlers.Commands.UpdateOrder;

public class UpdateOrderPayload
{
    public List<Product> Products { get; set; } = default!;
}