using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Products.Domain;

namespace Infrastructure.Persistence.EntityTypeConfigurations.Products;

public class ProductTypeConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(e => e.ProductId);

        builder.Property(e => e.Name).HasMaxLength(50).IsRequired();

        builder.HasOne(e => e.Storage).WithMany().HasForeignKey(e => e.StorageId);
    }
}