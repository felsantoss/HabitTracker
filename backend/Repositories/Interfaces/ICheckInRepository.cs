using Dtos.Pagination;
using Models.Habit;

namespace Repositories.Interfaces;

public interface ICheckInRepository
{
    Task<PagedResult<HabitCheckIn>> GetCheckInPaginated(int userId, int habitId, PaginationQuery pagination);
    
    Task<bool> CheckInAlreadyExists(int habitId, int userId, DateOnly date);
    
    Task Add(HabitCheckIn model);
}