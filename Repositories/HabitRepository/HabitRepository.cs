using Configuration.Data;
using Models.Habit;
using Repositories.Interfaces;

namespace Repositories.HabitRepository
{
	public class HabitRepository : IHabitRepository
	{
		private readonly DataContext _dataContext;

		public HabitRepository(DataContext dataContext)
		{
			_dataContext = dataContext;
		}

		public async Task Add(Habit habit)
		{
			_dataContext.Add(habit);

			await _dataContext.SaveChangesAsync();
		}
	}
}
