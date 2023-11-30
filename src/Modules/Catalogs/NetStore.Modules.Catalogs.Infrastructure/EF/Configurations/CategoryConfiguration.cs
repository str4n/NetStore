using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetStore.Modules.Catalogs.Domain.Category;

namespace NetStore.Modules.Catalogs.Infrastructure.EF.Configurations;

internal sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasConversion(x => x.Value, x => new(x))
            .IsRequired();
        
        builder.Property(x => x.Description)
            .HasConversion(x => x.Value, x => new(x))
            .IsRequired();
    }
}