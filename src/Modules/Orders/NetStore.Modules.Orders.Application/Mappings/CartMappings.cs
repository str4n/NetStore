using NetStore.Modules.Orders.Application.DTO;
using NetStore.Modules.Orders.Domain.Cart;

namespace NetStore.Modules.Orders.Application.Mappings;

internal static class CartMappings
{
    public static CartDto AsDto(this Cart cart)
        => new CartDto(cart.Products.Select(x => x.AsDto()));

    public static CartProductDto AsDto(this CartProduct cartProduct)
        => new CartProductDto(cartProduct.Product.Name, cartProduct.Product.CodeName, cartProduct.Product.Color,
            cartProduct.Product.Size, cartProduct.Quantity);
}