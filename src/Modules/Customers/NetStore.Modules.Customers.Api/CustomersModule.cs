using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetStore.Shared.Abstractions.Modules;

namespace NetStore.Modules.Customers.Api;

public sealed class CustomersModule : Module
{
    private const string BasePath = "customers-module";
    public override string Path => BasePath;
    public override void AddModule(IServiceCollection services, IConfiguration configuration)
    {
    }

    public override void UseModule(WebApplication app)
    {
    }
}