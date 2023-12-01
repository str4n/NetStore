using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetStore.Modules.Catalogs.Api.Endpoints;
using NetStore.Modules.Catalogs.Application;
using NetStore.Modules.Catalogs.Infrastructure;
using NetStore.Shared.Abstractions.Modules;

namespace NetStore.Modules.Catalogs.Api;

public sealed class CatalogsModule : Module
{
    public const string BasePath = "catalogs-module";
    public override string Path => BasePath;
    public override void AddModule(IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddApplication()
            .AddInfrastructure(configuration);
    }

    public override void UseModule(WebApplication app)
    {
        app
            .MapCategoryEmployeeEndpoints()
            .MapCategoryCustomerEndpoints();
    }
}