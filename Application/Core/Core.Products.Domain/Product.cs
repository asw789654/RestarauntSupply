using Core.Storages.Domain;

namespace Core.Products.Domain;

public class Product
{
    public int ProductId { get; set; }

    public string Name { get; set; } = default!;

    public int Volume { get; set; }

    bool IsDelivered { get; set; } = false;

    public DateTime? SpoilTime { get; set; }

    public int StorageId { get; set; }

    public Storage Storage { get; set; } = default!;
}