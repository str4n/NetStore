using Microsoft.Extensions.DependencyInjection;
using NetStore.Shared.Abstractions.Modules.Requests;
using NetStore.Shared.Infrastructure.Modules.Requests;

namespace NetStore.Shared.Infrastructure.Modules;

internal static class Extensions
{
    public static IServiceCollection AddModuleRequests(this IServiceCollection services)
    {
        services.AddSingleton<IModuleRequestDispatcher, ModuleRequestDispatcher>();
        
        var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName!.Contains("NetStore"));
        
        services.Scan(s =>
            s.FromAssemblies(assemblies)
                .AddClasses(c => c.AssignableTo(typeof(IModuleRequestHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

        return services;
    }
}