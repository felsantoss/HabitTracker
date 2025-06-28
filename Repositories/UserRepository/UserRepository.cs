using Configuration.Data;
using Dtos.Request.User;
using Microsoft.EntityFrameworkCore;
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
			try
			{
				_dataContext.Add(user);

				await _dataContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
		}

		public async Task<bool> UserAlreadyRegistered(string email)
		{
			try
			{
				var user = await _dataContext.Users.FirstOrDefaultAsync(f => f.Email == email);

				if (user == null)
					return false;

				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
		}
	}
}
