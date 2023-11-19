using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetStore.Modules.Products.Core.EF;
using NetStore.Modules.Products.Core.Services;
using NetStore.Shared.Infrastructure.Postgres;

namespace NetStore.Modules.Products.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEF(configuration);
        services.AddScoped<IProductsService, ProductsService>();
        
        return services;
    }
}