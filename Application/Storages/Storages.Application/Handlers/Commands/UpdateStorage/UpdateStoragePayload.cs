using Core.Products.Domain;

namespace Storages.Application.Handlers.Commands.UpdateStorage;

public class UpdateStoragePayload
{
    public string Name { get; set; } = default!;
    public int Capacity { get; set; }

    public int StorageTypeId { get; set; }
}