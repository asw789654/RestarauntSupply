using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orders.Domain;

namespace Infrastructure.Persistence.EntityTypeConfigurations.Orders;

public class OrderStatusTypeConfiguration : IEntityTypeConfiguration<OrderStatus>
{
    public void Configure(EntityTypeBuilder<OrderStatus> builder)
    {
        builder.HasKey(e => e.StatusId);
        builder.Property(e => e.StatusName).HasMaxLength(50).IsRequired();
    }
}