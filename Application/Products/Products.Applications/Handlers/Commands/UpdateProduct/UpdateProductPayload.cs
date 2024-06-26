namespace Products.Application.Handlers.Commands.UpdateProduct;

public class UpdateProductPayload
{
    public string Name { get; set; } = default!;

    public int Volume { get; set; }

    public DateTime? SpoilTime { get; set; }
}