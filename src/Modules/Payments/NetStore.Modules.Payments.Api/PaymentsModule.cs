using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetStore.Modules.Payments.Api.Endpoints;
using NetStore.Modules.Payments.Api.External;
using NetStore.Modules.Payments.Core;
using NetStore.Modules.Payments.Core.External;
using NetStore.Shared.Abstractions.Modules;
using NetStore.Shared.Infrastructure;

namespace NetStore.Modules.Payments.Api;

public sealed class PaymentsModule : Module
{
    public const string BasePath = "payments-module";
    public override string Path => BasePath;
    public override void AddModule(IServiceCollection services, IConfiguration configuration)
    {
        services.AddCore(configuration);

        var options = configuration.GetOptions<FakePaymentGatewayOptions>("PaymentIntegration");

        services.AddSingleton(options);
        services.AddScoped<IPaymentGatewayFacade, FakePaymentGatewayFacade>();
        services.AddHttpClient();
    }

    public override void UseModule(WebApplication app)
    {
        app.MapPaymentEndpoints();
    }
}