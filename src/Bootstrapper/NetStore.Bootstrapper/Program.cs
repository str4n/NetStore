using NetStore.Bootstrapper;
using NetStore.Modules.Catalogs.Api;
using NetStore.Modules.Customers.Api;
using NetStore.Modules.Notifications.Api;
using NetStore.Modules.Orders.Api;
using NetStore.Modules.Payments.Api;
using NetStore.Modules.Saga;
using NetStore.Modules.Users.Api;
using NetStore.Shared.Infrastructure;
using NetStore.Shared.Infrastructure.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseLogging();

ModuleLoader.Load<CatalogsModule>();
ModuleLoader.Load<UsersModule>();
ModuleLoader.Load<CustomersModule>();
ModuleLoader.Load<OrdersModule>();
ModuleLoader.Load<PaymentsModule>();
ModuleLoader.Load<NotificationsModule>();
ModuleLoader.Load<SagaModule>();

builder.Services
    .AddModules(builder.Configuration)
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseModules();

app.UseInfrastructure();

app.MapControllers();

app.Run();






namespace NetStore.Bootstrapper
{
    public partial class Program { }
}