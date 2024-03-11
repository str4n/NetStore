using Chronicle;
using NetStore.Modules.Customers.Shared.Commands;
using NetStore.Modules.Customers.Shared.DTO;
using NetStore.Modules.Customers.Shared.ModuleRequests;
using NetStore.Modules.Notifications.Shared.Commands;
using NetStore.Modules.Orders.Shared.Commands;
using NetStore.Modules.Orders.Shared.Events;
using NetStore.Modules.Orders.Shared.ModuleRequests;
using NetStore.Modules.Payments.Shared.Commands;
using NetStore.Modules.Payments.Shared.Events;
using NetStore.Shared.Abstractions.Messaging;

namespace NetStore.Modules.Saga.Sagas;

internal sealed class OrderSaga : Saga<OrderData>, ISagaStartAction<OrderPlaced>, ISagaAction<PaymentCompleted>, 
    ISagaAction<OrderCanceled>, ISagaAction<OrderProcessed>
{
    private readonly IMessageBroker _messageBroker;

    public OrderSaga(IMessageBroker messageBroker)
    {
        _messageBroker = messageBroker;
    }

    public override SagaId ResolveId(object message, ISagaContext context)
        => message switch
        {
            OrderPlaced m => m.OrderId.ToString(),
            PaymentCompleted m => GetOrderId(m.PaymentId).Result.ToString(),
            OrderCanceled m => m.OrderId.ToString(),
            OrderProcessed m => m.OrderId.ToString(),
            _ => base.ResolveId(message, context)
        };

    public async Task HandleAsync(OrderPlaced message, ISagaContext context)
    {
        Data.OrderId = message.OrderId;
        Data.CustomerId = message.CustomerId;
        Data.PaymentId = message.PaymentId;
        Data.Price = message.Price;

        await _messageBroker.PublishAsync(new ClearCart(Data.CustomerId));
        await _messageBroker.PublishAsync(new AddOrderToHistory(Data.OrderId));
        await _messageBroker.PublishAsync(new ClearCheckoutCart(Data.CustomerId));
        await _messageBroker.PublishAsync(new RequestPayment(Data.OrderId, Data.CustomerId, Data.PaymentId ,Data.Price));
    }
    
    public async Task HandleAsync(PaymentCompleted message, ISagaContext context)
    {
        await _messageBroker.PublishAsync(new ProcessOrder(Data.OrderId));
    }
    
    public async Task HandleAsync(OrderCanceled message, ISagaContext context)
    {
        //TODO: Send email
        
        await CompleteAsync();
    }
    
    public async Task HandleAsync(OrderProcessed message, ISagaContext context)
    {
        var customer = await _messageBroker
            .SendAsync<GetCustomerInformation, CustomerInformationDto>(new GetCustomerInformation(Data.CustomerId));

        await _messageBroker.PublishAsync(new SendOrderConfirmationEmail(customer.Email, Data.OrderId));
        await CompleteAsync();
    }

    public Task CompensateAsync(OrderPlaced message, ISagaContext context)
        => Task.CompletedTask;
    
    public Task CompensateAsync(PaymentCompleted message, ISagaContext context)
        => Task.CompletedTask;

    public Task CompensateAsync(OrderCanceled message, ISagaContext context)
        => Task.CompletedTask;

    public Task CompensateAsync(OrderProcessed message, ISagaContext context)
        => Task.CompletedTask;

    private  Task<Guid> GetOrderId(Guid paymentId)
        => _messageBroker.SendAsync<GetOrderId, Guid>(new GetOrderId(paymentId));
}

internal class OrderData
{
    public Guid OrderId { get; set; }
    public Guid CustomerId { get; set; }
    public Guid PaymentId { get; set; }
    public double Price { get; set; }
}