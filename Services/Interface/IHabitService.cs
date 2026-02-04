using Dtos.Pagination;
using Dtos.Request.Habit;
using Dtos.Response.CheckIn;
using Dtos.Response.Habit;

namespace Services.Interface
{
	public interface IHabitService
	{
		Task<PagedResult<HabitResponse>> Get(PaginationQuery request, int userId);
		
		Task<HabitResponse> Create(HabitCreateRequest habitCreateRequest, int userId);
		
		Task<CheckInResponse> GetCheckIn(int userId, int habitId);
		
		Task<bool> CreateCheckIn(int userId, int habitId);
	} 
}