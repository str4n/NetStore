using Microsoft.Extensions.DependencyInjection;
using NetStore.Shared.Abstractions.Messaging;

namespace NetStore.Shared.Infrastructure.Messaging;

internal static class Extensions
{
    public static IServiceCollection AddCrossModuleQueries(this IServiceCollection services)
    {
        services.AddSingleton<ICrossModuleQueryDispatcher, CrossModuleQueryDispatcher>();
        
        var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName!.Contains("NetStore"));
        
        services.Scan(s =>
            s.FromAssemblies(assemblies)
                .AddClasses(c => c.AssignableTo(typeof(ICrossModuleQueryHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

        return services;
    }

    public static IServiceCollection AddMessaging(this IServiceCollection services)
    {
        services.AddSingleton<IMessageBroker, InMemoryMessageBroker>();

        return services;
    }
}