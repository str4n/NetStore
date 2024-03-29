﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NetStore.Modules.Orders.Application.Commands;
using NetStore.Modules.Orders.Application.DTO;
using NetStore.Modules.Orders.Application.Queries;
using NetStore.Modules.Orders.Application.Storage;
using NetStore.Shared.Abstractions.Commands;
using NetStore.Shared.Abstractions.Queries;

namespace NetStore.Modules.Orders.Api.Endpoints.Checkout;

internal static class CheckoutCartCustomerEndpoints
{
    private const string Route = OrdersModule.BasePath + "/checkout";
    
    public static IEndpointRouteBuilder MapCheckoutCartCustomerEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet(Route, Get).RequireAuthorization();

        app.MapPut(Route + "/shipment", SetShipment).RequireAuthorization();
        app.MapPut(Route + "/payment", SetPayment).RequireAuthorization();

        app.MapPost(Route + "/place-order", PlaceOrder).RequireAuthorization();

        return app;
    }

    private static async Task<IResult> Get([FromServices] IQueryDispatcher queryDispatcher)
    {
        var checkout = await queryDispatcher.SendAsync<GetCheckoutCart, CheckoutCartDto>(new GetCheckoutCart());

        return Results.Ok(checkout);
    }

    private static async Task<IResult> SetShipment([FromBody] SetShipment command,
        [FromServices] ICommandDispatcher commandDispatcher)
    {
        await commandDispatcher.SendAsync(command);

        return Results.Ok();
    }
    
    private static async Task<IResult> SetPayment([FromBody] SetUpPayment command,
        [FromServices] ICommandDispatcher commandDispatcher)
    {
        await commandDispatcher.SendAsync(command);

        return Results.Ok();
    }

    private static async Task<IResult> PlaceOrder([FromServices] ICommandDispatcher commandDispatcher,
        [FromServices] IPaymentStorage paymentStorage)
    {
        await commandDispatcher.SendAsync(new PlaceOrder());

        return Results.Ok(paymentStorage.Get());
    }
}