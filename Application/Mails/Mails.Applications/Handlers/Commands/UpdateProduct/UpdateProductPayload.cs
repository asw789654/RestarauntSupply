namespace Mails.Applications.Handlers.Commands.UpdateProduct;

public class UpdateProductPayload
{
    public string Name { get; set; } = default!;

    public int Volume { get; set; }

    public DateTime? SpoilTime { get; set; }

    public int StorageTypeId { get; set; }
}