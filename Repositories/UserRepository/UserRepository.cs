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
		private readonly DataContext _dataContext = dataContext;

		public async Task Add(User user)
		{
			_dataContext.Add(user);

			await _dataContext.SaveChangesAsync();
		}

		public async Task Update(User user)
		{
			_dataContext.Update(user);

			await _dataContext.SaveChangesAsync();
		}

		public async Task<User> GetByIdAsync(int id)
		{
			var user = await _dataContext.Users.FirstOrDefaultAsync(f => f.Id == id)
				?? throw new Exception("UserNotFound");

			return user;
		}

		public async Task<User> GetByEmailAsync(string email)
		{
			var user = await _dataContext.Users.FirstOrDefaultAsync(f => f.Email == email)
				?? throw new Exception("UserNotFound");

			return user;
		}

		public async Task<bool> ExistsUserByEmailAsync(string email)
		{
			var user = await _dataContext.Users.FirstOrDefaultAsync(f => f.Email == email);

			if (user == null)
				return false;

			return true;
		}
	}
}
