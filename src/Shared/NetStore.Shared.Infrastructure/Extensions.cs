using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetStore.Shared.Infrastructure.Api;
using NetStore.Shared.Infrastructure.Exceptions;
using NetStore.Shared.Infrastructure.Postgres;

[assembly: InternalsVisibleTo("NetStore.Bootstrapper")]
namespace NetStore.Shared.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddExceptionHandling();

        services.ConfigurePostgres(configuration);
        
        services.AddControllers().ConfigureApplicationPartManager(manager =>
        {
            manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
        });

        return services;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseExceptionHandling();
        app.UseHttpsRedirection();
        app.UseRouting();

        return app;
    }

    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
    {
        var options = new T();
        configuration.GetSection(sectionName).Bind(options);

        return options;
    }
}