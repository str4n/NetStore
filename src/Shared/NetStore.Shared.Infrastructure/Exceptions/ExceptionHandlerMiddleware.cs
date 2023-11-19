using System.Net;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Shared.Infrastructure.Exceptions;

internal sealed class ExceptionHandlerMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger)
    {
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            await HandleExceptionAsync(exception, context);
        }
    }

    private static async Task HandleExceptionAsync(Exception exception, HttpContext context)
    {
        var (statusCode, error) = exception switch
        {
            NotFoundException => (HttpStatusCode.NotFound, 
                CreateError(exception)),
            ApiException => (HttpStatusCode.BadRequest, 
                CreateError(exception)),
            _ => (HttpStatusCode.InternalServerError, CreateError("error", "There was and server error."))
        };

        context.Response.StatusCode = (int)statusCode;
        await context.Response.WriteAsJsonAsync(error);
    }
    
    private static Error CreateError(Exception exception)
        => new Error(exception.GetType().Name.Replace("Exception", string.Empty).Underscore(), exception.Message);

    private static Error CreateError(string code, string reason) => new Error(code, reason);
}