using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NetStore.Modules.Payments.Core.Services;
using NetStore.Modules.Payments.Shared.DTO;

namespace NetStore.Modules.Payments.Api.Endpoints;

internal static class PaymentEndpoints
{
    private const string Route = PaymentsModule.BasePath + "/payment-webhook";
    
    public static IEndpointRouteBuilder MapPaymentEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost(Route, PaymentWebhook);

        return app;
    }

    private static async Task<IResult> PaymentWebhook(
        [FromBody] PaymentWebhookDto webhookDto, 
        [FromServices] IPaymentService paymentService)
    {
        await paymentService.OnPaymentPayed(webhookDto);
        return Results.StatusCode(StatusCodes.Status201Created);
    }
}