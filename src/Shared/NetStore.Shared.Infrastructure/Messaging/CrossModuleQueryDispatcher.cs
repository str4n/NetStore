using Microsoft.Extensions.DependencyInjection;
using NetStore.Shared.Abstractions.Messaging;
using NetStore.Shared.Abstractions.Queries;

namespace NetStore.Shared.Infrastructure.Messaging;

internal sealed class CrossModuleQueryDispatcher : ICrossModuleQueryDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public CrossModuleQueryDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResult> SendAsync<TResult>(ICrossModuleQuery<TResult> query)
    {
        using var scope = _serviceProvider.CreateScope();
        var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
        var handler = scope.ServiceProvider.GetRequiredService(handlerType);

        return await ((Task<TResult>) handlerType
            .GetMethod(nameof(IQueryHandler<IQuery<TResult>, TResult>.HandleAsync))
            ?.Invoke(handler, new[] {query}));
    }
}