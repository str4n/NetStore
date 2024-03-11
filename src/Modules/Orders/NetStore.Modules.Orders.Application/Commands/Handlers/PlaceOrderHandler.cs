using NetStore.Modules.Customers.Shared.ModuleRequests;
using NetStore.Modules.Orders.Application.Exceptions;
using NetStore.Modules.Orders.Domain.Repositories;
using NetStore.Modules.Orders.Shared.Events;
using NetStore.Shared.Abstractions.Commands;
using NetStore.Shared.Abstractions.Contexts;
using NetStore.Shared.Abstractions.Messaging;
using NetStore.Shared.Abstractions.Modules.Requests;
using NetStore.Shared.Abstractions.Time;

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
            await _messageBroker.SendAsync<GetIsCustomerInformationCompleted, bool>(new GetIsCustomerInformationCompleted(customerId));

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

        var orderPlacedEvent = new OrderPlaced(order.Id, customerId, order.Payment.Id, order.Payment.Amount);
        
        await _messageBroker.PublishAsync(orderPlacedEvent);
    }
}