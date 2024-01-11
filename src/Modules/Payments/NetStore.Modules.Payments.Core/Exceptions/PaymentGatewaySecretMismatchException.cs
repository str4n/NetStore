using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Payments.Core.Exceptions;

internal sealed class PaymentGatewaySecretMismatchException : ApiException
{
    public PaymentGatewaySecretMismatchException(Guid paymentId) : base($"Payment secret mismatch for payment with id: '{paymentId}'.", ExceptionCategory.ValidationError)
    {
    }
}