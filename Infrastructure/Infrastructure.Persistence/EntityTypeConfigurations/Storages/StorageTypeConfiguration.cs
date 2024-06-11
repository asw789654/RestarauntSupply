using Core.Products.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityTypeConfigurations.Storages;

public class StorageTypeConfiguration : IEntityTypeConfiguration<Storage>
{
    public void Configure(EntityTypeBuilder<Storage> builder)
    {
        builder.HasKey(e => e.StorageId);

        builder.Property(e => e.Name).HasMaxLength(50).IsRequired();

        builder.HasOne(e => e.StorageType).WithMany().HasForeignKey(e => e.StorageTypeId);
    }
}