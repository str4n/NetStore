using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Orders.Application.Exceptions;

internal sealed class InvalidPaymentMethodException : ApiException
{
    public InvalidPaymentMethodException() : base("Payment method is not valid. Correct payment methods: [ 'BankTransfer, 'CreditCard' ]", ExceptionCategory.ValidationError)
    {
    }
}