using Dtos.Request.Habit;
using Dtos.Request.User;
using Dtos.Response.Habit;

namespace Services.Interface
{
	public interface IHabitService
	{
		Task<HabitCreateResponse> Create(HabitCreateRequest habitCreateRequest, int userId);
	}
}
