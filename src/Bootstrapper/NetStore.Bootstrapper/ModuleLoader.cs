using NetStore.Shared.Abstractions.Modules;

namespace NetStore.Bootstrapper;

public static class ModuleLoader
{
    private static readonly Dictionary<string, Module> RegisteredModules = new();
    private static readonly ILogger _logger;

    static ModuleLoader()
    {
        var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
            builder.AddDebug();
        });

        _logger = loggerFactory.CreateLogger(typeof(ModuleLoader));
    }

    public static void Load<TModule>() where TModule : Module
    {
        var module = Activator.CreateInstance<TModule>();
        
        RegisteredModules.Add(module.Name, module);
    }

    public static IServiceCollection AddModules(this IServiceCollection services, IConfiguration configuration)
    {
        foreach (var module in RegisteredModules.Values)
        {
            module.AddModule(services, configuration);
            _logger.LogInformation("Added {name} module!", module.Name);
        }

        return services;
    }

    public static WebApplication UseModules(this WebApplication app)
    {
        foreach (var module in RegisteredModules.Values)
        {
            module.UseModule(app);
        }

        return app;
    }
}