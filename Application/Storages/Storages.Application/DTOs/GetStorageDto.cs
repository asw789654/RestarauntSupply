using Core.Application.Abstractions.Mappings;
using Core.Storages.Domain;

namespace Storages.Application.DTOs;

public class GetStorageDto : IMapFrom<Storage>
{
    public string Name { get; set; } = default!;
    public int StorageId { get; set; }

    public int Capacity { get; set; }

    public int StorageTypeId { get; set; }
}