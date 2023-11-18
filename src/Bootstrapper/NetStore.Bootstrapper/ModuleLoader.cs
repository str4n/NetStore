using NetStore.Shared.Abstractions.Modules;

namespace NetStore.Bootstrapper;

public static class ModuleLoader
{
    private static Dictionary<string, Module> _registeredModules = new();

    public static void RegisterModule<TModule>() where TModule : Module
    {
        var module = Activator.CreateInstance<TModule>();
        
        _registeredModules.Add(module.Name, module);
    }

    public static IServiceCollection AddModules(this IServiceCollection services, IConfiguration configuration)
    {
        foreach (var module in _registeredModules.Values)
        {
            module.AddModule(services, configuration);
        }

        return services;
    }

    public static WebApplication UseModules(this WebApplication app)
    {
        foreach (var module in _registeredModules.Values)
        {
            module.UseModule(app);
        }

        return app;
    }
}