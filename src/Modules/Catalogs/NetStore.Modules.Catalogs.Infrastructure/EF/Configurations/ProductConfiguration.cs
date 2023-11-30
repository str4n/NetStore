using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetStore.Modules.Catalogs.Domain.Product;

namespace NetStore.Modules.Catalogs.Infrastructure.EF.Configurations;

internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Name);

        builder.Property(x => x.Id).HasConversion(x => x.Value, x => new(x));

        builder.Property(x => x.Name).HasConversion(x => x.Value, x => new(x))
            .IsRequired();
        
        builder.Property(x => x.Description).HasConversion(x => x.Value, x => new(x))
            .IsRequired();
        
        builder.Property(x => x.Model).HasConversion(x => x.Value, x => new(x))
            .IsRequired();
        
        builder.Property(x => x.NetPrice).HasConversion(x => x.Value, x => new(x))
            .IsRequired();
        
        builder.Property(x => x.GrossPrice).HasConversion(x => x.Value, x => new(x))
            .IsRequired();
        
        builder.Property(x => x.Fabric).HasConversion(x => x.Value, x => new(x))
            .IsRequired();

        builder.OwnsOne(x => x.Weight);

        builder.HasOne(x => x.Category).WithMany();

        builder.HasOne(x => x.Brand).WithMany();
    }
}