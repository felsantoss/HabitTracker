using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Configuration.ExceptionHandle;

public class AuthenticatedUserContextMiddleware(RequestDelegate next)
{
    public const string UserIdItemKey = "AuthenticatedUserId";
    public const string UserIdentificationErrorMessage = "Não foi possível identificar o usuário.";

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
                throw new UnauthorizedException(UserIdentificationErrorMessage);

            context.Items[UserIdItemKey] = userId;
        }

        await next(context);
    }
}
