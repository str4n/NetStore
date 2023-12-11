using NetStore.Modules.Orders.Application.DTO;
using NetStore.Modules.Orders.Domain.Cart;
using NetStore.Modules.Orders.Domain.Shipment;

namespace NetStore.Modules.Orders.Application.Mappings;

internal static class CheckoutCartMappings
{
    public static CheckoutCartDto AsDto(this CheckoutCart checkoutCart)
        => new(
            checkoutCart.Payment.PaymentMethod.ToString(),
            checkoutCart.Shipment.AsDto(),
            checkoutCart.Products.Select(x => x.AsDto()));

    public static ShipmentDto AsDto(this Shipment shipment)
        => new(shipment.City, shipment.Street, shipment.PostalCode, shipment.ReceiverName);
}