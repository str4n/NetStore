using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Orders.Domain.Exceptions;

internal sealed class InvalidCardVerificationValueException : ApiException
{
    public InvalidCardVerificationValueException(string message) : base(message, ExceptionCategory.ValidationError)
    {
    }
}