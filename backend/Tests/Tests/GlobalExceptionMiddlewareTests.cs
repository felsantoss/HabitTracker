using System.Text.Json;
using Configuration.ExceptionHandle;
using Microsoft.AspNetCore.Http;

namespace Tests.Tests;

public class GlobalExceptionMiddlewareTests
{
    [Fact(DisplayName = "Should Return Bad Request Payload When Validation Exception Is Thrown")]
    public async Task Should_Return_Bad_Request_Payload_When_Validation_Exception_Is_Thrown()
    {
        var context = CreateHttpContext();
        var middleware = new GlobalExceptionMiddleware(_ => throw new ValidationException("Invalid request"));

        await middleware.InvokeAsync(context);

        var response = await ReadResponse(context);

        Assert.Equal(StatusCodes.Status400BadRequest, context.Response.StatusCode);
        Assert.Equal("application/json", context.Response.ContentType);
        Assert.NotNull(response);
        Assert.Equal(400, response!.Status);
        Assert.Equal("BadRequest", response.Error);
        Assert.Equal("Invalid request", response.Message);
        Assert.Equal("trace-validation", response.TraceId);
    }

    [Fact(DisplayName = "Should Return Unauthorized Payload When Unauthorized Exception Is Thrown")]
    public async Task Should_Return_Unauthorized_Payload_When_Unauthorized_Exception_Is_Thrown()
    {
        var context = CreateHttpContext();
        var middleware = new GlobalExceptionMiddleware(_ => throw new UnauthorizedException("Unauthorized access"));

        await middleware.InvokeAsync(context);

        var response = await ReadResponse(context);

        Assert.Equal(StatusCodes.Status401Unauthorized, context.Response.StatusCode);
        Assert.NotNull(response);
        Assert.Equal(401, response!.Status);
        Assert.Equal("Unauthorized", response.Error);
        Assert.Equal("Unauthorized access", response.Message);
        Assert.Equal("trace-validation", response.TraceId);
    }

    [Fact(DisplayName = "Should Return Generic Internal Server Error When Exception Is Not Mapped")]
    public async Task Should_Return_Generic_Internal_Server_Error_When_Exception_Is_Not_Mapped()
    {
        var context = CreateHttpContext();
        var middleware = new GlobalExceptionMiddleware(_ => throw new InvalidOperationException("sensitive details"));

        await middleware.InvokeAsync(context);

        var response = await ReadResponse(context);

        Assert.Equal(StatusCodes.Status500InternalServerError, context.Response.StatusCode);
        Assert.NotNull(response);
        Assert.Equal(500, response!.Status);
        Assert.Equal("InternalServerError", response.Error);
        Assert.Equal("An unexpected error occurred.", response.Message);
        Assert.DoesNotContain("sensitive details", response.Message);
        Assert.Equal("trace-validation", response.TraceId);
    }

    private static DefaultHttpContext CreateHttpContext()
    {
        var context = new DefaultHttpContext();
        context.TraceIdentifier = "trace-validation";
        context.Response.Body = new MemoryStream();

        return context;
    }

    private static async Task<ErrorResponse?> ReadResponse(HttpContext context)
    {
        context.Response.Body.Seek(0, SeekOrigin.Begin);

        return await JsonSerializer.DeserializeAsync<ErrorResponse>(context.Response.Body);
    }
}
