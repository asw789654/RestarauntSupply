using Core.Application.Abstractions.Mappings;
using Core.Mails.Domain;

namespace Mails.Applications.DTOs;

public class GetProductDto : IMapFrom<Mails>
{
    public Guid ProductId { get; set; }

    public string Name { get; set; } = default!;

    public int Volume { get; set; }

    public bool isDelivered { get; set; }

    public Guid OrderId { get; set; }

    public DateTime? SpoilTime { get; set; }

    public int StorageTypeId { get; set; }
}