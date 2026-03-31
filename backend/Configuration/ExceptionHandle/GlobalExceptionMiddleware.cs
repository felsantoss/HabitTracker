using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace Configuration.ExceptionHandle;

public class GlobalExceptionMiddleware(RequestDelegate next)
{
    private const string UnexpectedErrorMessage = "An unexpected error occurred.";

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, error, message) = MapException(exception);

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        var response = new ErrorResponse
        {
            Status = statusCode,
            Error = error,
            Message = message,
            TraceId = context.TraceIdentifier
        };

        var payload = JsonSerializer.Serialize(response);

        await context.Response.WriteAsync(payload);
    }

    private static (int StatusCode, string Error, string Message) MapException(Exception exception)
    {
        return exception switch
        {
            ValidationException => (StatusCodes.Status400BadRequest, "BadRequest", exception.Message),
            UnauthorizedException => (StatusCodes.Status401Unauthorized, "Unauthorized", exception.Message),
            ConflictException => (StatusCodes.Status409Conflict, "Conflict", exception.Message),
            _ => (StatusCodes.Status500InternalServerError, "InternalServerError", UnexpectedErrorMessage)
        };
    }
}
