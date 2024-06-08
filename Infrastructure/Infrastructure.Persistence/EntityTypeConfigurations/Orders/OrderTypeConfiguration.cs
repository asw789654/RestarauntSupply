using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orders.Domain;

namespace Infrastructure.Persistence.EntityTypeConfigurations.Orders;

public class OrderTypeConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(e => e.OrderId);

        builder.HasOne(e => e.User).WithMany().HasForeignKey(e => e.UserId);

        builder.HasOne(e => e.OrderStatus).WithMany().HasForeignKey(e => e.OrderStatusId);
    }
}