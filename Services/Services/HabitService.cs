using Configuration.ExceptionHandle;
using Dtos.Request.Habit;
using Dtos.Response.Habit;
using Models.Habit;
using Repositories.Interfaces;
using Services.Interface;
using Services.Validator;

namespace Services.Services
{
	public class HabitService(IHabitRepository habitRepository) : IHabitService
	{
		public async Task<HabitCreateResponse> Create(HabitCreateRequest habitCreateRequest, int userId)
		{
			HabitValidator.CreateHabitValidator(habitCreateRequest);
			
			var habitAlreadyExists = await habitRepository.HabitAlreadyExistsAsync(userId, habitCreateRequest.Title);
			
			if (habitAlreadyExists)
				throw new ExceptionHandler("Habit already exists");

			var habit = new Habit
			{
				Title = habitCreateRequest.Title,
				Description = habitCreateRequest.Description,
				UserId = userId,
				StartDate = habitCreateRequest.Date ?? DateTime.UtcNow,
				IsEnabled = true
			};
			
			await habitRepository.Add(habit);
			
			return new HabitCreateResponse
			{
				Id = habit.Id,
				Title =  habit.Title,
				Description = habit.Description,
				StartDate = habit.StartDate
			};
		}
	}
}
