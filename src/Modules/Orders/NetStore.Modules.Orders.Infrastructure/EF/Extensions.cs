using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetStore.Modules.Orders.Domain.Repositories;
using NetStore.Modules.Orders.Infrastructure.EF.Repositories;
using NetStore.Shared.Infrastructure.Postgres;

namespace NetStore.Modules.Orders.Infrastructure.EF;

internal static class Extensions
{
    public static IServiceCollection AddEF(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPostgres<OrdersDbContext>(configuration);
        
        services
            .AddScoped<IProductRepository, ProductRepository>()
            .AddScoped<ICartRepository, CartRepository>()
            .AddScoped<ICheckoutRepository, CheckoutRepository>();

        return services;
    }
}