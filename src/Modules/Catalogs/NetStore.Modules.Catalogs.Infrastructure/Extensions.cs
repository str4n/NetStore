using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetStore.Modules.Catalogs.Infrastructure.EF;

namespace NetStore.Modules.Catalogs.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEF(configuration);
        
        return services;
    }
}