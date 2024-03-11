using Microsoft.Extensions.DependencyInjection;
using NetStore.Modules.Catalogs.Application.Messaging;
using NetStore.Modules.Catalogs.Application.Services;
using NetStore.Shared.Infrastructure.Messaging;

namespace NetStore.Modules.Catalogs.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddSingleton<ISkuGenerator, SkuGenerator>();

        services.AddConsumer<DecreaseStockConsumer>();
        
        return services;
    }
}