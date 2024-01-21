using Microsoft.Extensions.Configuration;
using NetStore.Shared.Infrastructure;

namespace NetStore.Tests.Shared.Integration;

public sealed class OptionsProvider
{
    private const string AppSettings = "appsettings.test.json";
    
    private readonly IConfiguration _configuration = GetConfigurationRoot();

    public TOptions GetOptions<TOptions>(string sectionName) where TOptions : class, new()
        => _configuration.GetOptions<TOptions>(sectionName);

    public static IConfiguration GetConfigurationRoot()
        => new ConfigurationBuilder()
            .AddJsonFile(AppSettings, true)
            .AddEnvironmentVariables()
            .Build();
}