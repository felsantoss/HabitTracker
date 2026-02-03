using Models.Habit;

namespace Repositories.Interfaces;

public interface ICheckInRepository
{
    Task<bool> CheckInAlreadyExists(int habitId, int userId, DateOnly date);
    
    Task Add(HabitCheckIn model);
}