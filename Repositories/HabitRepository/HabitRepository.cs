using Configuration.Data;
using Microsoft.EntityFrameworkCore;
using Models.Habit;
using Repositories.Interfaces;

namespace Repositories.HabitRepository
{
	public class HabitRepository(DataContext dataContext) : IHabitRepository
	{
		public async Task<bool> HabitAlreadyExistsAsync(int userId, string title)
		{
			return await dataContext.Set<Habit>().AnyAsync(x => x.UserId == userId && x.Title == title);
		}
		
		public async Task Add(Habit habit)
		{
			dataContext.Add(habit);

			await dataContext.SaveChangesAsync();
		}
	}
}
