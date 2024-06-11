using Core.Application.Abstractions.Mappings;
using Core.Products.Domain;

namespace Storages.Application.DTOs;

public class GetStorageDto : IMapFrom<Storage>
{
    public string Name { get; set; } = default!;
    public int StorageId { get; set; }

    public int Capacity { get; set; }

    public int StorageTypeId { get; set; }

    public ICollection<Product> Products { get; set; } = default!;
}