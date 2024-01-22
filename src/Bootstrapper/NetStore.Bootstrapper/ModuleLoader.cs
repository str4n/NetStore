using NetStore.Shared.Abstractions.Modules;

namespace NetStore.Bootstrapper;

public static class ModuleLoader
{
    private static readonly List<ModuleRegistryEntry> ModuleRegistry = new();
    private static readonly ILogger Logger;

    static ModuleLoader()
    {
        var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
            builder.AddDebug();
        });

        Logger = loggerFactory.CreateLogger(typeof(ModuleLoader));
    }

    public static void Load<TModule>() where TModule : Module
    {
        var module = Activator.CreateInstance<TModule>();
        
        ModuleRegistry.Add(new ModuleRegistryEntry(module.Name, module));
    }

    public static IServiceCollection AddModules(this IServiceCollection services, IConfiguration configuration)
    {
        foreach (var entry in ModuleRegistry)
        {
            entry.Module.AddModule(services, configuration);
            Logger.LogInformation("Loaded {name} module!", entry.Name);
        }

        return services;
    }

    public static WebApplication UseModules(this WebApplication app)
    {
        foreach (var entry in ModuleRegistry)
        {
            entry.Module.UseModule(app);
        }

        return app;
    }

    private record ModuleRegistryEntry(string Name, Module Module);
}