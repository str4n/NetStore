using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NetStore.Modules.Orders.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }
}