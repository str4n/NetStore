using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NetStore.Shared.Infrastructure.Postgres;

public static class Extensions
{
    private const string SectionName = "Postgres";
    
    internal static IServiceCollection ConfigurePostgres(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection(SectionName);
        services.Configure<PostgresOptions>(section);
        
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        
        return services;
    }

    public static IServiceCollection AddPostgres<T>(this IServiceCollection services, IConfiguration configuration) where T : DbContext
    {
        var options = configuration.GetOptions<PostgresOptions>(SectionName);

        services.AddDbContext<T>(x => x.UseNpgsql(options.ConnectionString));
        
        return services;
    }
}