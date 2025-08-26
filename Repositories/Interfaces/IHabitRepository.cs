using Models.Habit;

namespace Repositories.Interfaces
{
	public interface IHabitRepository
	{
		Task Add(Habit habit);
	}
}
