using Microsoft.Extensions.DependencyInjection;
using NetStore.Shared.Abstractions.Events;

namespace NetStore.Shared.Infrastructure.Events;

internal static class Extensions
{
    public static IServiceCollection AddEvents(this IServiceCollection services)
    {
        services.AddSingleton<IEventDispatcher, EventDispatcher>();
        
        var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName!.Contains("NetStore"));

        services.Scan(s =>
            s.FromAssemblies(assemblies)
                .AddClasses(c => c.AssignableTo(typeof(IEventHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
        
        return services;
    }
}