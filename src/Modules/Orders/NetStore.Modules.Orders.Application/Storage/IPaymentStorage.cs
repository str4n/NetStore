namespace NetStore.Modules.Orders.Application.Storage;

public interface IPaymentStorage
{
    void Set(Payment payment);
    Payment Get();
}