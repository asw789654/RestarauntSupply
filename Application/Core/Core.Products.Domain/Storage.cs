using Core.Storages.Domain;

namespace Core.Products.Domain;

public class Storage
{
    public string Name { get; set; } = default!;
    public int StorageId { get; set; }

    public int Capacity { get; set; }

    public int StorageTypeId { get; set; }

    public StorageType StorageType { get; set; } = default!;

    public ICollection<Product> Products { get; } = new List<Product>();
}