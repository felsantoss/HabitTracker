using Configuration.ExceptionHandle;
using Dtos.Pagination;
using Dtos.Request.Habit;
using Dtos.Response.Habit;
using Models.Habit;
using Repositories.Interfaces;
using Services.Interface;
using Services.Validator;

namespace Services.Services
{
	public class HabitService(IHabitRepository habitRepository, 
		                      ICheckInRepository checkInRepository) : IHabitService
	{
		public async Task<PagedResult<HabitResponse>> Get(PaginationQuery request, int userId)
		{
			if (request == null)
				throw new ValidationException("Invalid request");
			
			var pagedResult = await habitRepository.GetPaginatedAsync(userId, request);

			return new PagedResult<HabitResponse>
			{
				PageNumber = pagedResult.PageNumber,
				PageSize = pagedResult.PageSize,
				TotalItems = pagedResult.TotalItems,
				Items = pagedResult.Items
					.Select(h => new HabitResponse
					{
						Id = h.Id,
						Description = h.Description,
						Title = h.Title,
						StartDate = h.StartDate
					})
					.ToList()
			};
		}
		
		public async Task<HabitResponse> Create(HabitCreateRequest habitCreateRequest, int userId)
		{
			HabitValidator.CreateHabitValidator(habitCreateRequest);
			
			var habitAlreadyExists = await habitRepository.HabitAlreadyExistsAsync(userId, habitCreateRequest.Title);
			
			if (habitAlreadyExists)
				throw new ValidationException("Habit already exists");

			var habit = new Habit
			{
				Title = habitCreateRequest.Title,
				Description = habitCreateRequest.Description,
				UserId = userId,
				StartDate = habitCreateRequest.Date ?? DateTime.UtcNow,
				IsEnabled = true
			};
			
			await habitRepository.Add(habit);
			
			return new HabitResponse
			{
				Id = habit.Id,
				Title =  habit.Title,
				Description = habit.Description,
				StartDate = habit.StartDate
			};
		}

		public async Task<bool> CheckIn(int userId, int habitId)
		{
			var habit = await habitRepository.GetHabitByIdAndUserId(habitId, userId);

			var currentDate = DateTime.UtcNow;
			
			var checkInAlreadyExists = await checkInRepository.CheckInAlreadyExists(habit.Id, userId, DateOnly.FromDateTime(currentDate));
			
			if (checkInAlreadyExists)
				throw new ValidationException("Check in already exists");

			var checkIn = new HabitCheckIn
			{
				HabitId = habitId,
				UserId = userId,
				Date = DateOnly.FromDateTime(currentDate),
				CreatAt = currentDate
			};
			
			await checkInRepository.Add(checkIn);	
			
			return true;
		} 
	}
}
