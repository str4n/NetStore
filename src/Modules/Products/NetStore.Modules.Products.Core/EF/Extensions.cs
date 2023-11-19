using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetStore.Modules.Products.Core.EF.Repositories;
using NetStore.Modules.Products.Core.Repositories;
using NetStore.Shared.Infrastructure.Postgres;

namespace NetStore.Modules.Products.Core.EF;

internal static class Extensions
{
    public static IServiceCollection AddEF(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPostgres<ProductsDbContext>(configuration);
        services.AddScoped<IProductsRepository, ProductsRepository>();

        return services;
    }
}