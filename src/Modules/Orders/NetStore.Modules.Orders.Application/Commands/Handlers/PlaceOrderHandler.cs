using NetStore.Modules.Orders.Application.Exceptions;
using NetStore.Modules.Orders.Application.Storage;
using NetStore.Modules.Orders.Domain.Repositories;
using NetStore.Modules.Orders.Shared.Events;
using NetStore.Shared.Abstractions.Commands;
using NetStore.Shared.Abstractions.Contexts;
using NetStore.Shared.Abstractions.Messaging;
using NetStore.Shared.Abstractions.Time;
using NetStore.Shared.Types.DTO;

namespace NetStore.Modules.Orders.Application.Commands.Handlers;

internal sealed class PlaceOrderHandler : ICommandHandler<PlaceOrder>
{
    private readonly ICheckoutRepository _checkoutRepository;
    private readonly IIdentityContext _identityContext;
    private readonly IClock _clock;
    private readonly IOrderRepository _orderRepository;
    private readonly IPaymentStorage _paymentStorage;
    private readonly IMessageBroker _messageBroker;

    public PlaceOrderHandler(ICheckoutRepository checkoutRepository, IIdentityContext identityContext, IClock clock, 
        IOrderRepository orderRepository, IPaymentStorage paymentStorage, IMessageBroker messageBroker)
    {
        _checkoutRepository = checkoutRepository;
        _identityContext = identityContext;
        _clock = clock;
        _orderRepository = orderRepository;
        _paymentStorage = paymentStorage;
        _messageBroker = messageBroker;
    }
    
    public async Task HandleAsync(PlaceOrder command)
    {
        var customerId = _identityContext.Id;
        var checkout = await _checkoutRepository.GetByCustomerId(customerId);
        
        if (checkout is null)
        {
            throw new CheckoutCartNotFoundException();
        }

        var order = checkout.PlaceOrder(_clock.Now());

        await _orderRepository.AddAsync(order);
        _paymentStorage.Set(new Payment(order.Payment.Id, order.Payment.PaymentMethod.ToString()));

        var @event = new OrderPlaced(customerId,
            new OrderDto(order.Id, order.Shipment.City, order.Shipment.Street, order.Shipment.PostalCode,
                order.Shipment.ReceiverName, order.PlaceDate,
                order.Lines.Select(x => new OrderLineDto(x.OrderLineNumber, x.Name, x.Quantity, x.UnitPrice))));

        await _messageBroker.PublishAsync(@event);
    }
}