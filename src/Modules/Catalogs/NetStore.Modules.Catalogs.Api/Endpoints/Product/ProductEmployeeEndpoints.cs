using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NetStore.Modules.Catalogs.Application.Commands;
using NetStore.Shared.Abstractions.Commands;
using NetStore.Shared.Infrastructure.Auth.Policies;

namespace NetStore.Modules.Catalogs.Api.Endpoints.Product;

internal static class ProductEmployeeEndpoints
{
    private const string Route = CatalogsModule.BasePath + "/products";
    
    public static IEndpointRouteBuilder MapProductEmployeeEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost(Route, Create).RequireAuthorization(Policies.AtLeastEmployee);
        app.MapPatch(Route + "/increase/{productId:guid}", IncreaseStock);
        app.MapPatch(Route + "/decrease/{productId:guid}", DecreaseStock);
        
        return app;
    }

    private static async Task<IResult> Create([FromBody] CreateProduct command,
        [FromServices] ICommandDispatcher commandDispatcher)
    {
        var id = Guid.NewGuid();
        await commandDispatcher.SendAsync(command with { Id = id});

        return Results.Created($"https://localhost:7240/{Route}/{id}", default);
    }

    private static async Task<IResult> IncreaseStock([FromRoute] Guid productId, [FromBody] IncreaseStockQuantity command,
        [FromServices] ICommandDispatcher commandDispatcher)
    {
        await commandDispatcher.SendAsync(command with { ProductId = productId });

        return Results.NoContent();
    }
    
    private static async Task<IResult> DecreaseStock([FromRoute] Guid productId,[FromBody] DecreaseStockQuantity command,
        [FromServices] ICommandDispatcher commandDispatcher)
    {
        await commandDispatcher.SendAsync(command with { ProductId = productId });

        return Results.NoContent();
    }
}