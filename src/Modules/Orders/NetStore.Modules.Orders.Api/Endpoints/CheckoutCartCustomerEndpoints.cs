using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NetStore.Modules.Orders.Application.DTO;
using NetStore.Modules.Orders.Application.Queries;
using NetStore.Shared.Abstractions.Queries;

namespace NetStore.Modules.Orders.Api.Endpoints;

internal static class CheckoutCartCustomerEndpoints
{
    private const string Route = OrdersModule.BasePath + "/checkout";
    
    public static IEndpointRouteBuilder MapCheckoutCartCustomerEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet(Route, Get).RequireAuthorization();

        return app;
    }

    private static async Task<IResult> Get([FromServices] IQueryDispatcher queryDispatcher)
    {
        var checkout = await queryDispatcher.SendAsync<GetCheckoutCart, CheckoutCartDto>(new GetCheckoutCart());

        return Results.Ok(checkout);
    }
}