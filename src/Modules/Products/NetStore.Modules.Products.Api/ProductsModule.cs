using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetStore.Modules.Products.Core;
using NetStore.Shared.Abstractions.Modules;

namespace NetStore.Modules.Products.Api;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class ProductsModule : Module
{
    public const string BasePath = "products-module";
    public override string Path => BasePath;
    public override void AddModule(IServiceCollection services, IConfiguration configuration)
    {
        services.AddCore();
    }

    public override void UseModule(WebApplication app)
    {
    }
}