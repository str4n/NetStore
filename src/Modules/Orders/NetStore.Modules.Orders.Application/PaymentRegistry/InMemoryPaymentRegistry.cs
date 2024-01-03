namespace NetStore.Modules.Orders.Application.PaymentRegistry;

internal sealed class InMemoryPaymentRegistry : IPaymentRegistry
{
    // TODO: db persistence
    
    private static readonly List<PaymentRegistryEntry> InMemoryRegistry = new();
    
    public Task Set(PaymentRegistryEntry entry)
    {
        InMemoryRegistry.Add(entry);
        return Task.CompletedTask;
    }

    public Task<PaymentRegistryEntry> Get(Guid paymentId)
        => Task.FromResult(InMemoryRegistry.SingleOrDefault(x => x.PaymentId == paymentId));
}