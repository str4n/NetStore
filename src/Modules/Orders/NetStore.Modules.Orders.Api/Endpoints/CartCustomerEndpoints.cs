using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NetStore.Modules.Orders.Application.Commands;
using NetStore.Modules.Orders.Application.DTO;
using NetStore.Modules.Orders.Application.Queries;
using NetStore.Shared.Abstractions.Commands;
using NetStore.Shared.Abstractions.Queries;

namespace NetStore.Modules.Orders.Api.Endpoints;

internal static class CartCustomerEndpoints
{
    private const string Route = OrdersModule.BasePath + "/cart";
    public static IEndpointRouteBuilder MapCartCustomerEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet(Route, GetCart).RequireAuthorization();
        app.MapPost(Route, AddProduct).RequireAuthorization();
        app.MapDelete(Route, RemoveProduct).RequireAuthorization();
        app.MapDelete(Route + "/clear", ClearCart).RequireAuthorization();

        return app;
    }

    private static async Task<IResult> GetCart([FromServices] IQueryDispatcher queryDispatcher)
    {
        var result = await queryDispatcher.SendAsync<GetCart, CartDto>(new GetCart());

        return Results.Ok(result);
    }

    private static async Task<IResult> AddProduct([FromBody] AddProductToCart command, 
        [FromServices]ICommandDispatcher commandDispatcher)
    {
        await commandDispatcher.SendAsync(command);

        return Results.NoContent();
    }

    private static async Task<IResult> RemoveProduct([AsParameters] RemoveProductFromCart command,
        [FromServices] ICommandDispatcher commandDispatcher)
    {
        await commandDispatcher.SendAsync(command);

        return Results.NoContent();
    }

    private static async Task<IResult> ClearCart([FromServices] ICommandDispatcher commandDispatcher)
    {
        await commandDispatcher.SendAsync(new ClearCart());
        
        return Results.NoContent();
    }
}