using Models.User;

namespace Repositories.Interfaces
{
	public interface IUserRepository
	{
		Task Add(User user);

		Task<bool> ExistsUserByEmailAsync(string email);
	}
}
