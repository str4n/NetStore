using Microsoft.Extensions.DependencyInjection;
using NetStore.Modules.Catalogs.Application.Events;
using NetStore.Modules.Catalogs.Application.Services;
using NetStore.Shared.Infrastructure.Messaging;

namespace NetStore.Modules.Catalogs.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddConsumer<OrderPlacedConsumer>();
        
        services
            .AddSingleton<ISkuGenerator, SkuGenerator>();
        
        return services;
    }
}