namespace NetStore.Shared.Abstractions.Domain;

public interface IDomainEventHandler<in TEvent> where TEvent : class, IDomainEvent
{
    Task HandleAsync(TEvent @event);
}