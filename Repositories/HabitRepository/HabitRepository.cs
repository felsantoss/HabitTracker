using Configuration.Data;
using Models.Habit;
using Repositories.Interfaces;

namespace Repositories.HabitRepository
{
	public class HabitRepository(DataContext dataContext) : IHabitRepository
	{
		public async Task Add(Habit habit)
		{
			dataContext.Add(habit);

			await dataContext.SaveChangesAsync();
		}
	}
}
