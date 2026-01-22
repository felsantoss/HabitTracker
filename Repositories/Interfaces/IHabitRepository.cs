using Models.Habit;

namespace Repositories.Interfaces
{
	public interface IHabitRepository
	{
		Task<bool> HabitAlreadyExistsAsync(int userId, string title);
		
		Task Add(Habit habit);
	}
}
