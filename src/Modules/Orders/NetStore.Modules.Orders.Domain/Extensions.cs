using Microsoft.Extensions.DependencyInjection;

namespace NetStore.Modules.Orders.Domain;

public static class Extensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        return services;
    }
}