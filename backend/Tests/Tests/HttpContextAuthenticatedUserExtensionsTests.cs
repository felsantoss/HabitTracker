using Configuration.ExceptionHandle;
using Microsoft.AspNetCore.Http;

namespace Tests.Tests;

public class HttpContextAuthenticatedUserExtensionsTests
{
    [Fact(DisplayName = "Should Return User Id From Http Context Items")]
    public void Should_Return_User_Id_From_Http_Context_Items()
    {
        var context = new DefaultHttpContext();
        context.Items[AuthenticatedUserContextMiddleware.UserIdItemKey] = 7;

        var result = context.GetAuthenticatedUserId();

        Assert.Equal(7, result);
    }

    [Fact(DisplayName = "Should Throw Unauthorized When User Id Is Missing From Http Context Items")]
    public void Should_Throw_Unauthorized_When_User_Id_Is_Missing_From_Http_Context_Items()
    {
        var context = new DefaultHttpContext();

        var exception = Assert.Throws<UnauthorizedException>(() => context.GetAuthenticatedUserId());

        Assert.Equal(AuthenticatedUserContextMiddleware.UserIdentificationErrorMessage, exception.Message);
    }
}
