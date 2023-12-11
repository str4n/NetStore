using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Orders.Domain.Exceptions;

internal sealed class CheckoutNotCompletedException : ApiException
{
    public CheckoutNotCompletedException() : base("Checkout is not completed.", ExceptionCategory.BadRequest)
    {
    }
}