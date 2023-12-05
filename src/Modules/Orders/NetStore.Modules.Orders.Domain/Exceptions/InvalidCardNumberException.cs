using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Orders.Domain.Exceptions;

internal sealed class InvalidCardNumberException : ApiException
{
    public InvalidCardNumberException(string message) : base(message, ExceptionCategory.ValidationError)
    {
    }
}