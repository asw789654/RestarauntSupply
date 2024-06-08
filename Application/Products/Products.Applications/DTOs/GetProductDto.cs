using Core.Application.Abstractions.Mappings;
using Core.Products.Domain;

namespace Products.Applications.DTOs;

public class GetProductDto : IMapFrom<Product>
{
    public int ProductId { get; set; }

    public string Name { get; set; } = default!;

    public int Volume { get; set; }

    public DateTime? SpoilTime { get; set; }

    public int StorageTypeId { get; set; }
}