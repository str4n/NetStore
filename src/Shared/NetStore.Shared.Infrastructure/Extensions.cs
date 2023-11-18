using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetStore.Shared.Infrastructure.Api;

[assembly: InternalsVisibleTo("NetStore.Bootstrapper")]
namespace NetStore.Shared.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers().ConfigureApplicationPartManager(manager =>
        {
            manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
        });

        return services;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseHttpsRedirection();
        app.UseRouting();
        app.MapControllers();

        return app;
    }
}