using NetStore.Modules.Orders.Application.DTO;
using NetStore.Modules.Orders.Domain.Cart;

namespace NetStore.Modules.Orders.Application.Mappings;

internal static class CartMappings
{
    public static CartDto AsDto(this Cart cart)
        => new CartDto(cart.Products.Select(x => new CartProductDto(x.Product.Name, x.Product.CodeName, x.Product.Color, x.Product.Size, x.Quantity)));
}