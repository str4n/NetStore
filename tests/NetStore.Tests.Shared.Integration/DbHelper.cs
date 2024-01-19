using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NetStore.Shared.Infrastructure;
using NetStore.Shared.Infrastructure.Postgres;

namespace NetStore.Tests.Shared.Integration;

public class DbHelper
{
    private const string SectionName = "Postgres";
    
    private static readonly IConfiguration Configuration = OptionsProvider.GetConfigurationRoot();

    public static DbContextOptions<T> GetOptions<T>() where T : DbContext
        => new DbContextOptionsBuilder<T>()
            .UseNpgsql(Configuration.GetOptions<PostgresOptions>(SectionName).ConnectionString)
            .EnableSensitiveDataLogging()
            .Options;
}