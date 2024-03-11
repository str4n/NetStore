using Chronicle;
using MassTransit;
using NetStore.Modules.Customers.Shared.Events;
using NetStore.Modules.Orders.Shared.Events;
using NetStore.Modules.Payments.Shared.Events;
using NetStore.Modules.Users.Shared.Events;

namespace NetStore.Modules.Saga;

internal sealed class MessagesHandler : IConsumer<UserSignedUp>, IConsumer<CustomerCreated>, IConsumer<CartCreated>,
    IConsumer<AccountActivated>, IConsumer<AccountActivationPrepared> ,IConsumer<PasswordRecoveryRequested>, 
    IConsumer<PasswordRecoveryPrepared>, IConsumer<PasswordRecovered>, IConsumer<OrderPlaced>, IConsumer<PaymentCompleted>,
    IConsumer<OrderCanceled>, IConsumer<OrderProcessed>, IConsumer<CheckoutCartCreated>
{
    private readonly ISagaCoordinator _coordinator;

    public MessagesHandler(ISagaCoordinator coordinator)
    {
        _coordinator = coordinator;
    }

    public async Task Consume(ConsumeContext<UserSignedUp> context)
        => await _coordinator.ProcessAsync(context.Message, SagaContext.Empty);

    public async Task Consume(ConsumeContext<CustomerCreated> context)
        => await _coordinator.ProcessAsync(context.Message, SagaContext.Empty);

    public async Task Consume(ConsumeContext<AccountActivated> context)
        => await _coordinator.ProcessAsync(context.Message, SagaContext.Empty);

    public async Task Consume(ConsumeContext<CartCreated> context)
        => await _coordinator.ProcessAsync(context.Message, SagaContext.Empty);

    public async Task Consume(ConsumeContext<AccountActivationPrepared> context)
        => await _coordinator.ProcessAsync(context.Message, SagaContext.Empty);
    
    public async Task Consume(ConsumeContext<PasswordRecoveryRequested> context)
        => await _coordinator.ProcessAsync(context.Message, SagaContext.Empty);

    public async Task Consume(ConsumeContext<PasswordRecoveryPrepared> context)
        => await _coordinator.ProcessAsync(context.Message, SagaContext.Empty);

    public async Task Consume(ConsumeContext<PasswordRecovered> context)
        => await _coordinator.ProcessAsync(context.Message, SagaContext.Empty);

    public async Task Consume(ConsumeContext<OrderPlaced> context)
        => await _coordinator.ProcessAsync(context.Message, SagaContext.Empty);

    public async Task Consume(ConsumeContext<PaymentCompleted> context)
        => await _coordinator.ProcessAsync(context.Message, SagaContext.Empty);

    public async Task Consume(ConsumeContext<OrderCanceled> context)
        => await _coordinator.ProcessAsync(context.Message, SagaContext.Empty);

    public async Task Consume(ConsumeContext<OrderProcessed> context)
        => await _coordinator.ProcessAsync(context.Message, SagaContext.Empty);

    public async Task Consume(ConsumeContext<CheckoutCartCreated> context)
        => await _coordinator.ProcessAsync(context.Message, SagaContext.Empty);
}