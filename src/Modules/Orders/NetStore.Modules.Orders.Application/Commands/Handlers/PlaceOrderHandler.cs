using NetStore.Modules.Orders.Application.Exceptions;
using NetStore.Modules.Orders.Application.Storage;
using NetStore.Modules.Orders.Domain.Repositories;
using NetStore.Modules.Orders.Shared.Events;
using NetStore.Shared.Abstractions.Commands;
using NetStore.Shared.Abstractions.Contexts;
using NetStore.Shared.Abstractions.Events;
using NetStore.Shared.Abstractions.Messaging;
using NetStore.Shared.Abstractions.Modules.Requests;
using NetStore.Shared.Abstractions.Time;
using NetStore.Shared.Types.DTO;
using NetStore.Shared.Types.ModuleRequests;

namespace NetStore.Modules.Orders.Application.Commands.Handlers;

internal sealed class PlaceOrderHandler : ICommandHandler<PlaceOrder>
{
    private readonly ICheckoutRepository _checkoutRepository;
    private readonly IIdentityContext _identityContext;
    private readonly IClock _clock;
    private readonly IOrderRepository _orderRepository;
    private readonly IMessageBroker _messageBroker;
    private readonly IModuleRequestDispatcher _moduleRequestDispatcher;

    public PlaceOrderHandler(ICheckoutRepository checkoutRepository, IIdentityContext identityContext, IClock clock, 
        IOrderRepository orderRepository, IMessageBroker messageBroker, 
        IModuleRequestDispatcher moduleRequestDispatcher)
    {
        _checkoutRepository = checkoutRepository;
        _identityContext = identityContext;
        _clock = clock;
        _orderRepository = orderRepository;
        _messageBroker = messageBroker;
        _moduleRequestDispatcher = moduleRequestDispatcher;
    }
    
    public async Task HandleAsync(PlaceOrder command)
    {
        var customerId = _identityContext.Id;

        var isCustomerInformationCompleted = 
            await _moduleRequestDispatcher.SendAsync<bool>(new GetIsCustomerInformationCompleted(customerId));

        if (!isCustomerInformationCompleted)
        {
            throw new CustomerInfoNotCompletedException();
        }
        
        var checkout = await _checkoutRepository.GetByCustomerId(customerId);
        
        if (checkout is null)
        {
            throw new CheckoutCartNotFoundException();
        }

        var order = checkout.PlaceOrder(_clock.Now());

        await _orderRepository.AddAsync(order);
        
        var tasks = new List<Task>();
        
        var orderPlacedEvent = new OrderPlaced(customerId,
            new OrderDto(order.Id, order.Shipment.City, order.Shipment.Street, order.Shipment.PostalCode,
                order.Shipment.ReceiverName, order.PlaceDate,
                order.Lines.Select(x => new OrderLineDto(x.OrderLineNumber, x.Name, x.Quantity, x.UnitPrice))));

        var orderPrice = order.Lines.Sum(x => x.UnitPrice * x.Quantity);
        
        var paymentRequestedEvent = new PaymentRequested(order.Payment.Id, order.Id, customerId, orderPrice, order.Payment.PaymentGatewaySecret);
        
        tasks.Add(_messageBroker.PublishAsync(orderPlacedEvent));
        tasks.Add(_messageBroker.PublishAsync(paymentRequestedEvent));

        await Task.WhenAll(tasks);
    }
}