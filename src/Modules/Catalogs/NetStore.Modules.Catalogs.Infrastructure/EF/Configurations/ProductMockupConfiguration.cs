using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetStore.Modules.Catalogs.Domain.Brand;
using NetStore.Modules.Catalogs.Domain.Category;
using NetStore.Modules.Catalogs.Domain.Product.Mockup;

namespace NetStore.Modules.Catalogs.Infrastructure.EF.Configurations;

internal sealed class ProductMockupConfiguration : IEntityTypeConfiguration<ProductMockup>
{
    public void Configure(EntityTypeBuilder<ProductMockup> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasConversion(x => x.Value, x => new(x))
            .IsRequired();
        
        builder.Property(x => x.Description).HasConversion(x => x.Value, x => new(x))
            .IsRequired();
        
        builder.Property(x => x.Model).HasConversion(x => x.Value, x => new(x))
            .IsRequired();
        
        builder.Property(x => x.Fabric).HasConversion(x => x.Value, x => new(x))
            .IsRequired();


        builder.HasOne<Category>().WithMany().HasForeignKey(x => x.CategoryId);

        builder.HasOne<Brand>().WithMany().HasForeignKey(x => x.BrandId);
    }
}