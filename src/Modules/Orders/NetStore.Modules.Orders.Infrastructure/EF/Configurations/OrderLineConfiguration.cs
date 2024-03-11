using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetStore.Modules.Orders.Domain.Order;

namespace NetStore.Modules.Orders.Infrastructure.EF.Configurations;

internal sealed class OrderLineConfiguration : IEntityTypeConfiguration<OrderLine>
{
    public void Configure(EntityTypeBuilder<OrderLine> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Quantity).IsRequired();
        builder.Property(x => x.UnitPrice).IsRequired();
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.SKU).IsRequired();
    }
}