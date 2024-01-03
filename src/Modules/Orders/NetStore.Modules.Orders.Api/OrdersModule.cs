using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetStore.Modules.Orders.Api.Endpoints;
using NetStore.Modules.Orders.Api.External;
using NetStore.Modules.Orders.Application;
using NetStore.Modules.Orders.Application.External;
using NetStore.Modules.Orders.Domain;
using NetStore.Modules.Orders.Infrastructure;
using NetStore.Shared.Abstractions.Modules;
using Extensions = NetStore.Shared.Infrastructure.Extensions;

namespace NetStore.Modules.Orders.Api;

public sealed class OrdersModule : Module
{
    public const string BasePath = "orders-module";
    public override string Path => BasePath;
    public override void AddModule(IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddApplication()
            .AddDomain()
            .AddInfrastructure(configuration);

        var options = Extensions.GetOptions<FakePaymentGatewayOptions>(configuration, "PaymentIntegration");

        services.AddSingleton(options);
        services.AddScoped<IPaymentGatewayFacade, FakePaymentGatewayFacade>();
        services.AddHttpClient();
    }

    public override void UseModule(WebApplication app)
    {
        app.MapCartCustomerEndpoints();

        app.MapCheckoutCartCustomerEndpoints();
    }
}