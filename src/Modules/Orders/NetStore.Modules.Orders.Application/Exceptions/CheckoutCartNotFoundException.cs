using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Orders.Application.Exceptions;

internal sealed class CheckoutCartNotFoundException : ApiException
{
    public CheckoutCartNotFoundException() : base("Checkout cart was not found.", ExceptionCategory.NotFound)
    {
    }
}