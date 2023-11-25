using Microsoft.Extensions.DependencyInjection;
using NetStore.Shared.Abstractions.Events;
using NetStore.Shared.Infrastructure.Attributes;

namespace NetStore.Shared.Infrastructure.Events;

internal static class Extensions
{
    public static IServiceCollection AddEvents(this IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName!.Contains("NetStore"));

        services.Scan(s => s.FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo(typeof(IEventHandler<>)).WithoutAttribute<DecoratorAttribute>())
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        
        services.AddSingleton<IEventDispatcher, EventDispatcher>();
        services.AddHostedService<EventDispatcher>();
        
        return services;
    }
}