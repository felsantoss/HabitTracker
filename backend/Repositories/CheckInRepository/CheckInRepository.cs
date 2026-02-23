using Configuration.Data;
using Dtos.Pagination;
using Microsoft.EntityFrameworkCore;
using Models.Habit;
using Repositories.Interfaces;

namespace Repositories.CheckInRepository;

public class CheckInRepository(DataContext dataContext) : ICheckInRepository
{
    public async Task<PagedResult<HabitCheckIn>> GetCheckInPaginated(int userId, int habitId, PaginationQuery pagination)
    {
        var query = dataContext.Set<HabitCheckIn>()
                               .AsNoTracking()
                               .Where(h => h.UserId == userId && h.HabitId == habitId)
                               .OrderByDescending(h => h.Date);
        
        var total = await query.CountAsync();
        
        var items = await query.Skip(pagination.Skip).Take(pagination.PageSize).ToListAsync();

        return new PagedResult<HabitCheckIn>()
        {
            PageNumber = pagination.PageNumber,
            PageSize = pagination.PageSize,
            TotalItems = total,
            Items = items
        };
    }
    
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