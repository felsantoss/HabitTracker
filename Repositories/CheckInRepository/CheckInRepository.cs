using Configuration.Data;
using Microsoft.EntityFrameworkCore;
using Models.Habit;
using Repositories.Interfaces;

namespace Repositories.CheckInRepository;

public class CheckInRepository(DataContext dataContext) : ICheckInRepository
{
    public async Task<bool> CheckInAlreadyExists(int habitId, int userId, DateOnly date)
    {
        return await dataContext.Set<HabitCheckIn>().AnyAsync(a => a.HabitId == habitId && a.UserId == userId && a.Date == date);
    }
    
    public async Task Add(HabitCheckIn model)
    {
        dataContext.Add(model);
        
        await dataContext.SaveChangesAsync();
    }
}