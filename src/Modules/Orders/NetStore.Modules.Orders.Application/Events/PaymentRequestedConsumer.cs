using MassTransit;
using NetStore.Modules.Orders.Application.External;
using NetStore.Modules.Orders.Application.PaymentRegistry;
using NetStore.Modules.Orders.Shared.Events;
using NetStore.Shared.Abstractions.Modules.Requests;
using NetStore.Shared.Types.DTO;
using NetStore.Shared.Types.ModuleRequests;

namespace NetStore.Modules.Orders.Application.Events;

internal sealed class PaymentRequestedConsumer : IConsumer<PaymentRequested>
{
    private readonly IPaymentRegistry _paymentRegistry;
    private readonly IPaymentGatewayFacade _paymentGatewayFacade;
    private readonly IModuleRequestDispatcher _moduleRequestDispatcher;

    public PaymentRequestedConsumer(IPaymentRegistry paymentRegistry, IPaymentGatewayFacade paymentGatewayFacade, IModuleRequestDispatcher moduleRequestDispatcher)
    {
        _paymentRegistry = paymentRegistry;
        _paymentGatewayFacade = paymentGatewayFacade;
        _moduleRequestDispatcher = moduleRequestDispatcher;
    }
    
    public async Task Consume(ConsumeContext<PaymentRequested> context)
    {
        var message = context.Message;

        await _paymentRegistry.Set(new PaymentRegistryEntry(message.PaymentId, message.OrderId));

        var customerInfo = await _moduleRequestDispatcher.SendAsync<CustomerInformationDto>(new GetCustomerInformation(message.CustomerId));

        var fullName = $"{customerInfo.FirstName} {customerInfo.LastName}";
        var address = $"{customerInfo.City} {customerInfo.Street}";
        
        await _paymentGatewayFacade.SetUpPayment(new PaymentDto(message.PaymentId, message.Amount, message.Secret), fullName, address);
    }
}