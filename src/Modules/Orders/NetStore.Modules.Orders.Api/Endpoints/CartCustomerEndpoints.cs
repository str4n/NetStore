using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NetStore.Modules.Orders.Application.Commands;
using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Orders.Api.Endpoints;

internal static class CartCustomerEndpoints
{
    private const string Route = OrdersModule.BasePath + "/cart";
    public static IEndpointRouteBuilder MapCartCustomerEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost(Route, AddProduct);

        return app;
    }

    private static async Task<IResult> AddProduct([FromBody] AddProductToCart command, 
        [FromServices]ICommandDispatcher commandDispatcher)
    {
        await commandDispatcher.SendAsync(command);

        return Results.NoContent();
    }
}