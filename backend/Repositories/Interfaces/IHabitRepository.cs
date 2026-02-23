using Dtos.Pagination;
using Models.Habit;

namespace Repositories.Interfaces
{
	public interface IHabitRepository
	{
		Task<PagedResult<Habit>> GetPaginatedAsync(int userId, PaginationQuery pagination);
		
		Task<bool> HabitAlreadyExistsAsync(int userId, string title);

		Task<Habit> GetHabitByIdAndUserId(int habitId, int userId);
		
		Task Add(Habit habit);
	}
}
