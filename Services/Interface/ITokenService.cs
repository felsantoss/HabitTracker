using Models.User;

namespace Services.Interface
{
	public interface ITokenService
	{
		string GenerateToken(User user);
	}
}
