using NetStore.Modules.Payments.Core.Exceptions;
using NetStore.Modules.Payments.Shared.DTO;
using NetStore.Shared.Abstractions.Time;

namespace NetStore.Modules.Payments.Core.Validators;

internal sealed class PaymentValidator
{
    private readonly IClock _clock;

    public PaymentValidator(IClock clock)
    {
        _clock = clock;
    }

    public void Validate(SetupPaymentDto paymentSetup)
    {
        if (paymentSetup is null)
        {
            throw new InvalidPaymentValueException(nameof(paymentSetup));
        }

        if (paymentSetup.PaymentId.Equals(Guid.Empty))
        {
            throw new InvalidPaymentValueException(nameof(paymentSetup.PaymentId));
        }
        
        if (paymentSetup.CustomerId.Equals(Guid.Empty))
        {
            throw new InvalidPaymentValueException(nameof(paymentSetup.CustomerId));
        }

        if (paymentSetup.Amount is default(double) or <= 0)
        {
            throw new InvalidPaymentValueException(nameof(paymentSetup.Amount));
        }
        
        if (paymentSetup.DueDate <= _clock.Now())
        {
            throw new InvalidPaymentValueException(nameof(paymentSetup.DueDate));
        }
    }
}