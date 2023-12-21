using Microsoft.Extensions.DependencyInjection;
using NetStore.Modules.Catalogs.Application.Services;

namespace NetStore.Modules.Catalogs.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddSingleton<ISkuGenerator, SkuGenerator>();
        
        return services;
    }
}