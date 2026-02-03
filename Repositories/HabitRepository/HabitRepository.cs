using System.ComponentModel.DataAnnotations;
using Configuration.Data;
using Dtos.Pagination;
using Microsoft.EntityFrameworkCore;
using Models.Habit;
using Repositories.Interfaces;

namespace Repositories.HabitRepository
{
	public class HabitRepository(DataContext dataContext) : IHabitRepository
	{
		public async Task<PagedResult<Habit>> GetPaginatedAsync(int userId, PaginationQuery pagination)
		{
			var query = dataContext.Set<Habit>()
								   .Where(h => h.UserId == userId)
								   .OrderBy(h => h.StartDate);
			
			var total = await query.CountAsync();

			var habits = await query.Skip(pagination.Skip).Take(pagination.PageSize).ToListAsync();

			return new PagedResult<Habit>()
			{
				PageNumber = pagination.PageNumber,
				PageSize = pagination.PageSize,
				TotalItems = total,
				Items = habits
			};
		}
		
		public async Task<bool> HabitAlreadyExistsAsync(int userId, string title)
		{
			return await dataContext.Set<Habit>().AnyAsync(x => x.UserId == userId && x.Title == title);
		}
		
		public async Task<Habit> GetHabitByIdAndUserId(int habitId, int userId)
		{
			var query = await dataContext.Set<Habit>().FirstOrDefaultAsync(f => f.Id == habitId && f.UserId == userId) 
			            ?? throw new ValidationException("Habit not found");
			
			return query;
		}
		
		public async Task Add(Habit habit)
		{
			dataContext.Add(habit);

			await dataContext.SaveChangesAsync();
		}
	}
}
