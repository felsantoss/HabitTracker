using Microsoft.AspNetCore.Http;

namespace Configuration.ExceptionHandle;

public static class HttpContextAuthenticatedUserExtensions
{
    public static int GetAuthenticatedUserId(this HttpContext context)
    {
        if (context.Items.TryGetValue(AuthenticatedUserContextMiddleware.UserIdItemKey, out var userId) &&
            userId is int authenticatedUserId)
        {
            return authenticatedUserId;
        }

        throw new UnauthorizedException(AuthenticatedUserContextMiddleware.UserIdentificationErrorMessage);
    }
}
