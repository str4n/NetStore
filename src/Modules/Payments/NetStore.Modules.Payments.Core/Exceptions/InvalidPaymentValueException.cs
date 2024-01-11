using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Payments.Core.Exceptions;

internal sealed class InvalidPaymentValueException : ApiException
{
    public InvalidPaymentValueException(string valueName) : base($"{valueName} is invalid.", ExceptionCategory.ValidationError)
    {
    }
}