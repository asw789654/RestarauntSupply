using Core.Storages.Domain;

namespace Core.Products.Domain;

public class Product
{
    public Guid ProductId { get; set; }

    public string Name { get; set; } = default!;

    public int Volume { get; set; }

    public bool IsDelivered { get; set; } = false;

    public DateTime? MailTime { get; set; }

    public Guid? OrderId { get; set; }

    public DateTime? SpoilTime { get; set; }

    public int? StorageId { get; set; }

    public Storage? Storage { get; set; }

}