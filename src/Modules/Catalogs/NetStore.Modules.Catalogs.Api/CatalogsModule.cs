using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetStore.Modules.Catalogs.Api.Endpoints;
using NetStore.Modules.Catalogs.Api.Endpoints.Brand;
using NetStore.Modules.Catalogs.Api.Endpoints.Category;
using NetStore.Modules.Catalogs.Api.Endpoints.Product;
using NetStore.Modules.Catalogs.Api.Endpoints.ProductMockup;
using NetStore.Modules.Catalogs.Application;
using NetStore.Modules.Catalogs.Domain;
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
            .AddDomain()
            .AddInfrastructure(configuration);
    }

    public override void UseModule(WebApplication app)
    {
        app
            .MapCategoryAdminEndpoints()
            .MapCategoryCustomerEndpoints();

        app
            .MapBrandAdminEndpoints()
            .MapBrandCustomerEndpoints();

        app.MapProductMockupEndpoints();

        app.MapProductAdminEndpoints();
    }
}