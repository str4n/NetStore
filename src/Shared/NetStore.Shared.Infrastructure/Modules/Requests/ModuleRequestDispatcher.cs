using Microsoft.Extensions.DependencyInjection;
using NetStore.Shared.Abstractions.Modules.Requests;

namespace NetStore.Shared.Infrastructure.Modules.Requests;

internal sealed class ModuleRequestDispatcher : IModuleRequestDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public ModuleRequestDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public async Task<TResult> SendAsync<TResult>(IModuleRequest request)
    {
        using var scope = _serviceProvider.CreateScope();
        var handlerType = typeof(IModuleRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResult));
        var handler = scope.ServiceProvider.GetRequiredService(handlerType);
        
        var result = await (Task<TResult>) handlerType
            .GetMethod(nameof(IModuleRequestHandler<IModuleRequest<TResult>, TResult>.HandleAsync))
            ?.Invoke(handler, new[] {request});

        return result;
    }
}