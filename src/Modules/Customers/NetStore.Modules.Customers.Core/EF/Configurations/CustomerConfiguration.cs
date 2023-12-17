using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetStore.Modules.Customers.Core.Domain.Customer;
using Newtonsoft.Json;

namespace NetStore.Modules.Customers.Core.EF.Configurations;

internal sealed class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Email)
            .HasConversion(x => x.Value, x => new(x))
            .IsRequired();

        builder.Property(x => x.FirstName)
            .HasConversion(x => x.Value, x => new(x));

        builder.Property(x => x.LastName)
            .HasConversion(x => x.Value, x => new(x));

        builder.Property(x => x.CustomerStatus).HasConversion<string>();

        builder.HasMany(x => x.Orders).WithOne();

        builder.OwnsOne(x => x.Address);
    }
}