using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetStore.Modules.Products.Core.EF;
using NetStore.Shared.Infrastructure.Postgres;

namespace NetStore.Modules.Products.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPostgres<ProductsDbContext>(configuration);
        
        return services;
    }
}