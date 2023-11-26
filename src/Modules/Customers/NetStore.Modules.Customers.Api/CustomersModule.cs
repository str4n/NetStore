using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetStore.Modules.Customers.Api.Endpoints;
using NetStore.Modules.Customers.Core;
using NetStore.Shared.Abstractions.Modules;

namespace NetStore.Modules.Customers.Api;

public sealed class CustomersModule : Module
{
    public const string BasePath = "customers-module";
    public override string Path => BasePath;
    public override void AddModule(IServiceCollection services, IConfiguration configuration)
    {
        services.AddCore(configuration);
    }

    public override void UseModule(WebApplication app)
    {
        app.MapCustomerEndpoints();
    }
}