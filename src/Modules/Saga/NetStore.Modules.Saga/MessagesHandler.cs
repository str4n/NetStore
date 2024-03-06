using Chronicle;
using MassTransit;
using NetStore.Modules.Customers.Shared.Events;
using NetStore.Modules.Orders.Shared.Events;
using NetStore.Modules.Users.Shared.Events;

namespace NetStore.Modules.Saga;

internal sealed class MessagesHandler : IConsumer<UserSignedUp>, IConsumer<CustomerCreated>, IConsumer<CartCreated>,IConsumer<AccountActivated>
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
}