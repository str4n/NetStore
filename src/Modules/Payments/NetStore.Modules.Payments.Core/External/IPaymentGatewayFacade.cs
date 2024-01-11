using NetStore.Modules.Payments.Shared.DTO;

namespace NetStore.Modules.Payments.Core.External;

public interface IPaymentGatewayFacade
{
    Task SetUpPayment(SetupPaymentDto payment, string fullName, string address, string paymentGatewaySecret);
}