using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetStore.Modules.Catalogs.Domain.Repositories;
using NetStore.Modules.Catalogs.Infrastructure.EF.Repositories;
using NetStore.Shared.Infrastructure.Postgres;

namespace NetStore.Modules.Catalogs.Infrastructure.EF;

internal static class Extensions
{
    public static IServiceCollection AddEF(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPostgres<CatalogsDbContext>(configuration);

        services.AddUnitOfWork<ICatalogsUnitOfWork, CatalogsUnitOfWork>();
        
        services
            .AddScoped<ICategoryRepository, CategoryRepository>()
            .AddScoped<IBrandRepository, BrandRepository>()
            .AddScoped<IProductRepository, ProductRepository>()
            .AddScoped<IProductMockupRepository, ProductMockupRepository>();

        return services;
    }
}