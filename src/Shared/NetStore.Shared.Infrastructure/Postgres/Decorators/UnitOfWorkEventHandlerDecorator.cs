using Microsoft.Extensions.DependencyInjection;
using NetStore.Shared.Abstractions.Commands;
using NetStore.Shared.Abstractions.Events;
using NetStore.Shared.Infrastructure.Attributes;

namespace NetStore.Shared.Infrastructure.Postgres.Decorators;

[Decorator]
internal class UnitOfWorkEventHandlerDecorator<T> : IEventHandler<T> where T : class, IEvent
{
    private readonly IEventHandler<T> _eventHandler;
    private readonly UnitOfWorkTypeRegistry _unitOfWorkTypeRegistry;
    private readonly IServiceProvider _serviceProvider;

    public UnitOfWorkEventHandlerDecorator(IEventHandler<T> eventHandler, UnitOfWorkTypeRegistry unitOfWorkTypeRegistry, IServiceProvider serviceProvider)
    {
        _eventHandler = eventHandler;
        _unitOfWorkTypeRegistry = unitOfWorkTypeRegistry;
        _serviceProvider = serviceProvider;
    }
    
    public async Task HandleAsync(T @event)
    {
        var handlerType = (_serviceProvider.GetService<IEventHandler<T>>() as UnitOfWorkEventHandlerDecorator<T>)?._eventHandler.GetType();
        var unitOfWorkType = _unitOfWorkTypeRegistry.Resolve(handlerType);

        if (unitOfWorkType is null)
        {
            await _eventHandler.HandleAsync(@event);
            return;
        }

        var unitOfWork = _serviceProvider.GetRequiredService(unitOfWorkType) as IUnitOfWork;
        await unitOfWork?.ExecuteAsync(() => _eventHandler.HandleAsync(@event))!;
    }
}