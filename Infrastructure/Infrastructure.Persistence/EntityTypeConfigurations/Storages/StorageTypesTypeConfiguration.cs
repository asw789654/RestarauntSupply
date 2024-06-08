using Core.Storages.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityTypeConfigurations.Storages;

public class StorageTypesTypeConfiguration : IEntityTypeConfiguration<StorageType>
{
    public void Configure(EntityTypeBuilder<StorageType> builder)
    {
        builder.HasKey(e => e.StorageTypeId);

        builder.Property(e => e.Name).HasMaxLength(50).IsRequired();

    }
}