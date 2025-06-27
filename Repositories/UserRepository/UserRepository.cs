using Configuration.Data;
using Dtos.Request.User;
using Models.User;
using Repositories.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace Repositories.UserRepository
{
	[ExcludeFromCodeCoverage]
	public class UserRepository : IUserRepository
	{
		private readonly DataContext _dataContext;

		public UserRepository(DataContext dataContext)
		{
			_dataContext = dataContext;
		}

		public async Task Add(User user)
		{
			_dataContext.Add(user);

			await _dataContext.SaveChangesAsync();
		}

		public async Task<bool> UserAlreadyRegistred(string email)
		{
			var user = await _dataContext.Users.FindAsync(email);

			if (user == null)
				return false;

			return true;
		}
	}
}
