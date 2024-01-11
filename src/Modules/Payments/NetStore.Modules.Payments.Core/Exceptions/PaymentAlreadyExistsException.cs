using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Payments.Core.Exceptions;

internal sealed class PaymentAlreadyExistsException : ApiException
{
    public PaymentAlreadyExistsException(Guid id) : base($"Payment with id: '{id}' already exists.", ExceptionCategory.AlreadyExists)
    {
    }
}