using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetStore.Modules.Orders.Domain.Cart;
using NetStore.Modules.Orders.Domain.Payment;
using NetStore.Modules.Orders.Domain.Shipment;
using Newtonsoft.Json;

namespace NetStore.Modules.Orders.Infrastructure.EF.Configurations;

internal sealed class CheckoutCartConfiguration : IEntityTypeConfiguration<CheckoutCart>
{
    public void Configure(EntityTypeBuilder<CheckoutCart> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.CustomerId).IsRequired();

        builder.HasMany(x => x.Products);
        
        builder.Property(x => x.Shipment).HasConversion(
            x => JsonConvert.SerializeObject(x),
            x => JsonConvert.DeserializeObject<Shipment>(x));
        
        builder.Property(x => x.Payment).HasConversion(
            x => JsonConvert.SerializeObject(x),
            x => JsonConvert.DeserializeObject<Payment>(x));
    }
}