using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Orders.Domain.Exceptions;

internal sealed class CannotCheckoutEmptyCartException : ApiException
{
    public CannotCheckoutEmptyCartException() : base("Cannot checkout empty cart.", ExceptionCategory.BadRequest)
    {
    }
}