using Microsoft.Extensions.DependencyInjection;

namespace NetStore.Modules.Catalogs.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}