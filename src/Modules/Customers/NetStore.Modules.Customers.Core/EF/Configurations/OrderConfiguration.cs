using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetStore.Modules.Customers.Core.Domain.Order;

namespace NetStore.Modules.Customers.Core.EF.Configurations;

internal sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.Lines).WithOne();

        builder.OwnsOne(x => x.Address);
    }
}