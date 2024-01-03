using NetStore.Shared.Types.DTO;

namespace NetStore.Modules.Orders.Application.External;

public interface IPaymentGatewayFacade
{
    Task SetUpPayment(PaymentDto payment, string fullName, string address);
}