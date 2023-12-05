namespace NetStore.Modules.Orders.Domain.Payment;

public sealed record PaymentCard
{
    public Guid Id { get; private set; }
    public CardNumber CardNumber { get; private set; }
    public DateOnly ExpirationDate { get; private set; }
    public CVV CVV { get; private set; }

    public PaymentCard(CardNumber cardNumber, DateOnly expirationDate, CVV cvv)
    {
        Id = Guid.NewGuid();
        ExpirationDate = expirationDate;
        CVV = cvv;
    }
}