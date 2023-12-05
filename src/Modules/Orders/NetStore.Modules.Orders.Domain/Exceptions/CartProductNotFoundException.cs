using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Orders.Domain.Exceptions;

internal sealed class CartProductNotFoundException : ApiException
{
    public CartProductNotFoundException() : base("Cart product was not found.", ExceptionCategory.NotFound)
    {
    }
}