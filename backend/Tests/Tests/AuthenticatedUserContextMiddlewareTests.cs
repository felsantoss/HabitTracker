using System.Security.Claims;
using Configuration.ExceptionHandle;
using Microsoft.AspNetCore.Http;

namespace Tests.Tests;

public class AuthenticatedUserContextMiddlewareTests
{
    [Fact(DisplayName = "Should Store User Id When Authenticated Request Has Valid Claim")]
    public async Task Should_Store_User_Id_When_Authenticated_Request_Has_Valid_Claim()
    {
        var context = CreateAuthenticatedContext("12");
        var middleware = new AuthenticatedUserContextMiddleware(_ => Task.CompletedTask);

        await middleware.InvokeAsync(context);

        Assert.True(context.Items.TryGetValue(AuthenticatedUserContextMiddleware.UserIdItemKey, out var userId));
        Assert.Equal(12, userId);
    }

    [Fact(DisplayName = "Should Throw Unauthorized When Authenticated Request Has Missing Claim")]
    public async Task Should_Throw_Unauthorized_When_Authenticated_Request_Has_Missing_Claim()
    {
        var context = CreateAuthenticatedContext(null);
        var middleware = new AuthenticatedUserContextMiddleware(_ => Task.CompletedTask);

        var exception = await Assert.ThrowsAsync<UnauthorizedException>(() => middleware.InvokeAsync(context));

        Assert.Equal(AuthenticatedUserContextMiddleware.UserIdentificationErrorMessage, exception.Message);
    }

    [Fact(DisplayName = "Should Throw Unauthorized When Authenticated Request Has Invalid Claim")]
    public async Task Should_Throw_Unauthorized_When_Authenticated_Request_Has_Invalid_Claim()
    {
        var context = CreateAuthenticatedContext("abc");
        var middleware = new AuthenticatedUserContextMiddleware(_ => Task.CompletedTask);

        var exception = await Assert.ThrowsAsync<UnauthorizedException>(() => middleware.InvokeAsync(context));

        Assert.Equal(AuthenticatedUserContextMiddleware.UserIdentificationErrorMessage, exception.Message);
    }

    [Fact(DisplayName = "Should Ignore Request When User Is Not Authenticated")]
    public async Task Should_Ignore_Request_When_User_Is_Not_Authenticated()
    {
        var context = new DefaultHttpContext();
        context.User = new ClaimsPrincipal(new ClaimsIdentity());
        var middleware = new AuthenticatedUserContextMiddleware(_ => Task.CompletedTask);

        await middleware.InvokeAsync(context);

        Assert.False(context.Items.ContainsKey(AuthenticatedUserContextMiddleware.UserIdItemKey));
    }

    private static DefaultHttpContext CreateAuthenticatedContext(string? userId)
    {
        var claims = new List<Claim>();

        if (userId is not null)
            claims.Add(new Claim(ClaimTypes.NameIdentifier, userId));

        var identity = new ClaimsIdentity(claims, "Bearer");

        return new DefaultHttpContext
        {
            User = new ClaimsPrincipal(identity)
        };
    }
}
