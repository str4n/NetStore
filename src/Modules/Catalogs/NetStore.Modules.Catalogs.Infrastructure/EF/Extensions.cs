using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetStore.Shared.Infrastructure.Postgres;

namespace NetStore.Modules.Catalogs.Infrastructure.EF;

internal static class Extensions
{
    public static IServiceCollection AddEF(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPostgres<CatalogsDbContext>(configuration);

        return services;
    }
}