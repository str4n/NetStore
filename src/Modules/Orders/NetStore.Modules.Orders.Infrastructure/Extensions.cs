using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetStore.Modules.Orders.Infrastructure.EF;
using NetStore.Modules.Orders.Infrastructure.Services;

namespace NetStore.Modules.Orders.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHostedService<OrderProcessor>();
        services.AddScoped<OrderProcessor>();
        
        services.AddEF(configuration);
        
        return services;
    }
}