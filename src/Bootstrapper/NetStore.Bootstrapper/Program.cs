using NetStore.Bootstrapper;
using NetStore.Modules.Products.Api;
using NetStore.Shared.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

ModuleLoader.Load<ProductsModule>();

builder.Services
    .AddModules(builder.Configuration)
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseModules();

app.UseInfrastructure();

app.Run();