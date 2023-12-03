using Microsoft.Extensions.DependencyInjection;

namespace NetStore.Modules.Orders.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}