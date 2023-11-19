using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using NetStore.Shared.Infrastructure.Api;
using NetStore.Shared.Infrastructure.Exceptions;
using NetStore.Shared.Infrastructure.Postgres;
using NetStore.Shared.Infrastructure.Services;

[assembly: InternalsVisibleTo("NetStore.Bootstrapper")]
namespace NetStore.Shared.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddExceptionHandling();

        services.ConfigurePostgres(configuration);

        services.AddHostedService<DatabaseInitializer>();
        
        services.AddControllers().ConfigureApplicationPartManager(manager =>
        {
            manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
        });

        services.AddSwaggerGen(swagger =>
        {
            swagger.CustomSchemaIds(x => x.FullName);
            swagger.SwaggerDoc("v1", new OpenApiInfo()
            {
                Title = "NetStore API",
                Version = "v1"
            });
        });

        return services;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseExceptionHandling();

        app.UseSwagger();
        app.UseReDoc(options =>
        {
            options.RoutePrefix = "docs";
            options.DocumentTitle = "NetStore API";
            options.SpecUrl("/swagger/v1/swagger.json");
        });
        
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