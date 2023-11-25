using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetStore.Shared.Abstractions.Commands;
using NetStore.Shared.Abstractions.Events;
using NetStore.Shared.Infrastructure.Postgres.Decorators;
using Scrutor;

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

    internal static IServiceCollection AddTransactionalDecorators(this IServiceCollection services)
    {
        services.Decorate(typeof(IEventHandler<>), typeof(UnitOfWorkEventHandlerDecorator<>));
        services.Decorate(typeof(ICommandHandler<>), typeof(UnitOfWorkCommandHandlerDecorator<>));

        return services;
    }

    public static IServiceCollection AddPostgres<T>(this IServiceCollection services, IConfiguration configuration) where T : DbContext
    {
        var options = configuration.GetOptions<PostgresOptions>(SectionName);

        services.AddDbContext<T>(x => x.UseNpgsql(options.ConnectionString));

        services.AddSingleton(new UnitOfWorkTypeRegistry());
        
        return services;
    }

    public static IServiceCollection AddUnitOfWork<TUnitOfWork, TImplementation>(this IServiceCollection services)
    where TUnitOfWork: class, IUnitOfWork where TImplementation : class, TUnitOfWork
    {
        services.AddScoped<TUnitOfWork, TImplementation>();
        services.AddScoped<IUnitOfWork, TImplementation>();

        using var serviceProvider = services.BuildServiceProvider();
        serviceProvider.GetRequiredService<UnitOfWorkTypeRegistry>().Register<TUnitOfWork>();

        return services;
    }
}