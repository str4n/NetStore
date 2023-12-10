using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Orders.Application.Exceptions;

internal sealed class NotEnoughProductsInCartException : ApiException
{
    public NotEnoughProductsInCartException(int quantity) : base($"There is less than {quantity} products in cart", ExceptionCategory.BadRequest)
    {
    }
}