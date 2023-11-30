using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetStore.Modules.Catalogs.Domain.Brand;

namespace NetStore.Modules.Catalogs.Infrastructure.EF.Configurations;

internal sealed class BrandConfiguration : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasConversion(x => x.Value, x => new(x))
            .IsRequired();
    }
}