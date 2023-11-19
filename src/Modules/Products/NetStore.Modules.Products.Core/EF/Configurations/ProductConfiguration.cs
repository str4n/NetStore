using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetStore.Modules.Products.Core.Domain.Entities;
using NetStore.Modules.Products.Core.Domain.ValueObjects;

namespace NetStore.Modules.Products.Core.EF.Configurations;

internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasConversion(x => x.Value, x => new(x))
            .IsRequired();
        
        builder.Property(x => x.Description)
            .HasConversion(x => x.Value, x => new(x))
            .IsRequired();
        
        builder.Property(x => x.TruePrice)
            .HasConversion(x => x.Value, x => new(x))
            .IsRequired()
            .HasColumnName("Price");

        builder.Property(x => x.Discount)
            .HasConversion(x => x.Value, x => new(x));

        builder.Property(x => x.Categories)
            .HasColumnType("jsonb")
            .HasConversion(x => JsonSerializer.Serialize(x, GetJsonOptions()), 
                x => JsonSerializer.Deserialize<IEnumerable<Category>>(x, GetJsonOptions()));
    }

    private static JsonSerializerOptions GetJsonOptions()
        => new()
        {
            PropertyNameCaseInsensitive = true
        };
}