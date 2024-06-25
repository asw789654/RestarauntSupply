namespace Core.Storages.Domain;

public class Storage
{
    public string Name { get; set; } = default!;
    public int StorageId { get; set; }

    public int Capacity { get; set; }

    public int StorageTypeId { get; set; }

    public StorageType StorageType { get; set; } = default!;
}