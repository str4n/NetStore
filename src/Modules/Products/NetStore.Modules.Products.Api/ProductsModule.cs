using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetStore.Shared.Abstractions.Modules;

namespace NetStore.Modules.Products.Api;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class ProductsModule : Module
{
    public override void AddModule(IServiceCollection services, IConfiguration configuration)
    {
        
    }

    public override void UseModule(WebApplication app)
    {
        
    }
}