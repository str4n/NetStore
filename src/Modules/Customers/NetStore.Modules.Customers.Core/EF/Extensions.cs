using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetStore.Modules.Customers.Core.EF.Repositories;
using NetStore.Modules.Customers.Core.Repositories;
using NetStore.Shared.Infrastructure.Postgres;

namespace NetStore.Modules.Customers.Core.EF;

internal static class Extensions
{
    public static IServiceCollection AddEF(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPostgres<CustomersDbContext>(configuration);
        services.AddScoped<ICustomersRepository, CustomersRepository>();

        return services;
    }
}