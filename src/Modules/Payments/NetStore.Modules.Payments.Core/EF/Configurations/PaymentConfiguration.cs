using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetStore.Modules.Payments.Core.Domain;

namespace NetStore.Modules.Payments.Core.EF.Configurations;

internal sealed class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(x => x.PaymentId);
        
        builder.Property(x => x.PaymentGatewaySecret).IsRequired();
        builder.Property(x => x.Payed).IsRequired();
    }
}