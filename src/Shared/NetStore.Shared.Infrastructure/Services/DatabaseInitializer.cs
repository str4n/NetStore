using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace NetStore.Shared.Infrastructure.Services;

internal sealed class DatabaseInitializer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<DatabaseInitializer> _logger;

    public DatabaseInitializer(IServiceProvider serviceProvider, ILogger<DatabaseInitializer> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var dbContextTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(x => x.IsAssignableTo(typeof(DbContext)) && !x.IsInterface && x != typeof(DbContext));

        using var scope = _serviceProvider.CreateScope();
        
        foreach (var dbContextType in dbContextTypes)
        {
            var dbContext = scope.ServiceProvider.GetService(dbContextType) as DbContext;

            if (dbContext is not null)
            {
                await dbContext.Database.MigrateAsync(cancellationToken);
                _logger.LogInformation("{dbContext} initialized.", dbContext.GetType());
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}