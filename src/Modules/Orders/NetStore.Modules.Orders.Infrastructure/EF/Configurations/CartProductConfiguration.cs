using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetStore.Modules.Orders.Domain.Cart;

namespace NetStore.Modules.Orders.Infrastructure.EF.Configurations;

internal sealed class CartProductConfiguration : IEntityTypeConfiguration<CartProduct>
{
    public void Configure(EntityTypeBuilder<CartProduct> builder)
    {
        builder.HasKey(x => new {x.CartId, x.ProductId});
        builder.HasOne(x => x.Product);
    }
}