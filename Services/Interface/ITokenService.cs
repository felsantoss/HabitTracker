using Dtos.Response.Token;
using Models.User;

namespace Services.Interface
{
	public interface ITokenService
	{
		TokenResponse GenerateToken(User user);
	}
}
