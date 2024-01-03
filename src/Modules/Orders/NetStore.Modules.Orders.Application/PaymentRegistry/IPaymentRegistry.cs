namespace NetStore.Modules.Orders.Application.PaymentRegistry;

public interface IPaymentRegistry
{
    Task Set(PaymentRegistryEntry entry);
    Task<PaymentRegistryEntry> Get(Guid paymentId);
}