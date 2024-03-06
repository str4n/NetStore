using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetStore.Shared.Abstractions.Modules;

namespace NetStore.Modules.Saga;

public sealed class SagaModule : Module
{
    public const string BasePath = "saga-module";
    public override string Path => BasePath;
    public override void AddModule(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSaga();
    }

    public override void UseModule(WebApplication app)
    {
    }
}