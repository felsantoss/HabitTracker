using Models.User;

namespace Repositories.Interfaces
{
	public interface IUserRepository
	{
		Task Add(User user);

		Task Update(User user);

		Task<User> GetByIdAsync(int id);

		Task<User> GetByEmailAsync(string email);

		Task<bool> ExistsUserByEmailAsync(string email);
	}
}
