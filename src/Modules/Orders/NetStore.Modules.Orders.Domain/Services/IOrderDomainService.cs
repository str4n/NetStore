namespace NetStore.Modules.Orders.Domain.Services;

public interface IOrderDomainService
{
    Task PayOrder(Guid orderId);
}