namespace NetStore.Modules.Orders.Domain.Payment;

public sealed record Payment(Guid Id, PaymentMethod PaymentMethod);

// Simple implementation. Will we replaced with stripe.
public enum PaymentMethod
{
    BankTransfer,
    CreditCard
}