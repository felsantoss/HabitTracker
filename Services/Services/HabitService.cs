using Dtos.Request.Habit;
using Dtos.Request.User;
using Dtos.Response.Habit;
using Repositories.Interfaces;
using Services.Interface;

namespace Services.Services
{
	public class HabitService(IHabitRepository habitRepository) : IHabitService
	{
		//public async Task<HabitCreateResponse> Create(HabitCreateRequest habitCreateRequest, int userId)
		//{

		//}
	}
}
