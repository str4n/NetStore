using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Orders.Domain.Exceptions;

internal sealed class InvalidShipmentException : ApiException
{
    public InvalidShipmentException(string message) : base(message, ExceptionCategory.ValidationError)
    {
    }
}