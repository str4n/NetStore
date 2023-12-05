using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Orders.Domain.Exceptions;

internal sealed class CannotChangeOrderStatusException : ApiException
{
    public CannotChangeOrderStatusException(string message) : base(message, ExceptionCategory.BadRequest)
    {
    }
}