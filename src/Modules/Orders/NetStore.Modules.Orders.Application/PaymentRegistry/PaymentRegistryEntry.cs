namespace NetStore.Modules.Orders.Application.PaymentRegistry;

public record PaymentRegistryEntry(Guid PaymentId, Guid OrderId);