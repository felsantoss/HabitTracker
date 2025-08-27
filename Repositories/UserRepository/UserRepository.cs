using Configuration.Data;
using Microsoft.EntityFrameworkCore;
using Models.User;
using Repositories.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace Repositories.UserRepository
{
	[ExcludeFromCodeCoverage]
	public class UserRepository(DataContext dataContext) : IUserRepository
	{
		public async Task Add(User user)
		{
			dataContext.Add(user);

			await dataContext.SaveChangesAsync();
		}

		public async Task Update(User user)
		{
			dataContext.Update(user);

			await dataContext.SaveChangesAsync();
		}

		public async Task<User> GetByIdAsync(int id)
		{
			var user = await dataContext.Users.FirstOrDefaultAsync(f => f.Id == id)
				?? throw new Exception("UserNotFound");

			return user;
		}

		public async Task<User> GetByEmailAsync(string email)
		{
			var user = await dataContext.Users.FirstOrDefaultAsync(f => f.Email == email)
				?? throw new Exception("UserNotFound");

			return user;
		}

		public async Task<bool> ExistsUserByEmailAsync(string email)
		{
			var user = await dataContext.Users.FirstOrDefaultAsync(f => f.Email == email);

			if (user == null)
				return false;

			return true;
		}
	}
}
