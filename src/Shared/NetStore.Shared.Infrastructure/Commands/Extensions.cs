using Microsoft.Extensions.DependencyInjection;
using NetStore.Shared.Abstractions.Commands;
using NetStore.Shared.Infrastructure.Attributes;

namespace NetStore.Shared.Infrastructure.Commands;

internal static class Extensions
{
    public static IServiceCollection AddCommands(this IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName!.Contains("NetStore"));

        services.Scan(s => s.FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)).WithoutAttribute<DecoratorAttribute>())
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        
        services.AddSingleton<ICommandDispatcher, CommandDispatcher>();

        return services;
    }
}