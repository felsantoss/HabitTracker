using Dtos.Request.Login;
using Dtos.Response.Token;

namespace Services.Interface
{
	public interface IAuthenticationService
	{
		Task<TokenResponse> Authentication(LoginRequest loginRequest);
	}
}
