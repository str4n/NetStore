using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Orders.Domain.Exceptions;

internal sealed class InvalidOrderLineDataException : ApiException
{
    public InvalidOrderLineDataException(string message) : base(message, ExceptionCategory.ValidationError)
    {
    }
}