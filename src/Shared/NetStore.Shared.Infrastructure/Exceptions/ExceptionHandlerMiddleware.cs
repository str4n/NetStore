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
            ApiException apiException => CreateError(apiException),
            _ => (HttpStatusCode.InternalServerError, CreateError("error", "there was an error"))
        };

        context.Response.StatusCode = (int)statusCode;
        await context.Response.WriteAsJsonAsync(error);
    }

    private static (HttpStatusCode, Error) CreateError(ApiException exception)
    {
        var error = new Error(exception.GetType().Name.Replace("Exception", string.Empty).Underscore(), exception.Message);

        return exception.ExceptionCategory switch
        {
            ExceptionCategory.NotFound => (HttpStatusCode.NotFound, error),
            ExceptionCategory.ValidationError => (HttpStatusCode.BadRequest, error),
            ExceptionCategory.AlreadyExists => (HttpStatusCode.BadRequest, error),
            ExceptionCategory.BadRequest => (HttpStatusCode.BadRequest, error),
            _ => (HttpStatusCode.BadRequest, error)
        };
    }

    private static Error CreateError(string code, string reason) => new Error(code, reason);
}