using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetStore.Modules.Orders.Domain.Order;
using NetStore.Modules.Orders.Domain.Payment;
using NetStore.Modules.Orders.Domain.Shipment;
using Newtonsoft.Json;

namespace NetStore.Modules.Orders.Infrastructure.EF.Configurations;

internal sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new(x));

        builder.HasMany(x => x.Lines);
        
        builder.Property(x => x.Shipment).HasConversion(
            x => JsonConvert.SerializeObject(x),
            x => JsonConvert.DeserializeObject<Shipment>(x));
        
        builder.Property(x => x.Payment).HasConversion(
            x => JsonConvert.SerializeObject(x),
            x => JsonConvert.DeserializeObject<Payment>(x));

        builder.Property(x => x.Status).HasConversion<string>();
    }
}