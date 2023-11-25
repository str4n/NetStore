using Microsoft.Extensions.DependencyInjection;
using NetStore.Shared.Abstractions.Commands;
using NetStore.Shared.Infrastructure.Attributes;

namespace NetStore.Shared.Infrastructure.Postgres.Decorators;

[Decorator]
internal class UnitOfWorkCommandHandlerDecorator<T> : ICommandHandler<T> where T : class, ICommand
{
    private readonly ICommandHandler<T> _commandHandler;
    private readonly UnitOfWorkTypeRegistry _unitOfWorkTypeRegistry;
    private readonly IServiceProvider _serviceProvider;

    public UnitOfWorkCommandHandlerDecorator(ICommandHandler<T> commandHandler, UnitOfWorkTypeRegistry unitOfWorkTypeRegistry, IServiceProvider serviceProvider)
    {
        _commandHandler = commandHandler;
        _unitOfWorkTypeRegistry = unitOfWorkTypeRegistry;
        _serviceProvider = serviceProvider;
    }
    
    public async Task HandleAsync(T command)
    {
        var handlerType = (_serviceProvider.GetService<ICommandHandler<T>>() as UnitOfWorkCommandHandlerDecorator<T>)?._commandHandler.GetType();
        var unitOfWorkType = _unitOfWorkTypeRegistry.Resolve(handlerType);

        if (unitOfWorkType is null)
        {
            await _commandHandler.HandleAsync(command);
            return;
        }

        var unitOfWork = _serviceProvider.GetRequiredService(unitOfWorkType) as IUnitOfWork;
        await unitOfWork?.ExecuteAsync(() => _commandHandler.HandleAsync(command))!;
    }
}