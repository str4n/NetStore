using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetStore.Modules.Users.Core.EF.Repositories;
using NetStore.Modules.Users.Core.Repositories;
using NetStore.Shared.Infrastructure.Postgres;

namespace NetStore.Modules.Users.Core.EF;

internal static class Extensions
{
    public static IServiceCollection AddEF(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPostgres<UsersDbContext>(configuration);
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddUnitOfWork<IUsersUnitOfWork, UsersUnitOfWork>();

        return services;
    }
}