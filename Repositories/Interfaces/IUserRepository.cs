using Dtos.Request.User;
using Models.User;

namespace Repositories.Interfaces
{
	public interface IUserRepository
	{
		Task Add(User user);

		Task<bool> UserAlreadyRegistred(string email);
	}
}
