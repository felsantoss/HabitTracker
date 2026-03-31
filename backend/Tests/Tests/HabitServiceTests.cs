using Dtos.Pagination;
using Dtos.Request.Habit;
using Models.Habit;
using Moq;
using Repositories.Interfaces;
using Services.Services;
using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace Tests.Tests
{
	public class HabitServiceTests
	{
		private readonly Mock<IHabitRepository> _habitRepositoryMock;
		private readonly Mock<ICheckInRepository> _checkInRepositoryMock;

		public HabitServiceTests()
		{
			_habitRepositoryMock = new Mock<IHabitRepository>();
			_checkInRepositoryMock = new Mock<ICheckInRepository>();
		}

		[Fact(DisplayName = "Should Update Habit When User Is Owner")]
		public async Task Should_Update_Habit_When_User_Is_Owner()
		{
			// Arrange
			var service = new HabitService(_habitRepositoryMock.Object, _checkInRepositoryMock.Object);
			var request = new HabitUpdateRequest
			{
				Title = "Read book",
				Description = "Read 20 pages"
			};
			var habit = new Habit
			{
				Id = 10,
				UserId = 1,
				Title = "Old title",
				Description = "Old description",
				StartDate = DateTime.UtcNow,
				IsEnabled = true
			};

			_habitRepositoryMock.Setup(x => x.GetHabitByIdAndUserId(10, 1)).ReturnsAsync(habit);
			_habitRepositoryMock.Setup(x => x.Update(It.IsAny<Habit>())).Returns(Task.CompletedTask);

			// Act
			var result = await service.Update(1, 10, request);

			// Assert
			Assert.Equal("Read book", result.Title);
			Assert.Equal("Read 20 pages", result.Description);
			_habitRepositoryMock.Verify(x => x.Update(It.Is<Habit>(h => h.Id == 10 && h.Title == "Read book" && h.Description == "Read 20 pages")), Times.Once);
		}

		[Fact(DisplayName = "Should Archive Habit When User Is Owner")]
		public async Task Should_Archive_Habit_When_User_Is_Owner()
		{
			// Arrange
			var service = new HabitService(_habitRepositoryMock.Object, _checkInRepositoryMock.Object);
			var habit = new Habit
			{
				Id = 11,
				UserId = 1,
				Title = "Train",
				Description = "Workout",
				StartDate = DateTime.UtcNow,
				IsEnabled = true
			};

			_habitRepositoryMock.Setup(x => x.GetHabitByIdAndUserId(11, 1)).ReturnsAsync(habit);
			_habitRepositoryMock.Setup(x => x.Archive(It.IsAny<Habit>())).Callback<Habit>(h => h.IsEnabled = false).Returns(Task.CompletedTask);

			// Act
			await service.Archive(1, 11);

			// Assert
			Assert.False(habit.IsEnabled);
			_habitRepositoryMock.Verify(x => x.Archive(It.Is<Habit>(h => h.Id == 11)), Times.Once);
		}

		[Fact(DisplayName = "Should Fail Update Habit From Another User")]
		public async Task Should_Fail_Update_Habit_From_Another_User()
		{
			// Arrange
			var service = new HabitService(_habitRepositoryMock.Object, _checkInRepositoryMock.Object);
			var request = new HabitUpdateRequest
			{
				Title = "Read",
				Description = "Read daily"
			};

			_habitRepositoryMock
				.Setup(x => x.GetHabitByIdAndUserId(20, 1))
				.ThrowsAsync(new ValidationException("Habit not found"));

			// Act & Assert
			await Assert.ThrowsAsync<ValidationException>(() => service.Update(1, 20, request));
		}

		[Fact(DisplayName = "Should Fail Archive Habit From Another User")]
		public async Task Should_Fail_Archive_Habit_From_Another_User()
		{
			// Arrange
			var service = new HabitService(_habitRepositoryMock.Object, _checkInRepositoryMock.Object);

			_habitRepositoryMock
				.Setup(x => x.GetHabitByIdAndUserId(30, 1))
				.ThrowsAsync(new ValidationException("Habit not found"));

			// Act & Assert
			await Assert.ThrowsAsync<ValidationException>(() => service.Archive(1, 30));
		}

		[Fact(DisplayName = "Should Not Return Archived Habit In Active Listing")]
		public async Task Should_Not_Return_Archived_Habit_In_Active_Listing()
		{
			// Arrange
			var service = new HabitService(_habitRepositoryMock.Object, _checkInRepositoryMock.Object);
			var pagination = new PaginationQuery { PageNumber = 1, PageSize = 10 };
			var habits = new List<Habit>
			{
				new()
				{
					Id = 40,
					UserId = 1,
					Title = "Meditate",
					Description = "10 minutes",
					StartDate = DateTime.UtcNow,
					IsEnabled = true
				}
			};

			_habitRepositoryMock.Setup(x => x.GetHabitByIdAndUserId(40, 1))
				.ReturnsAsync(() =>
				{
					return habits.FirstOrDefault(h => h.Id == 40 && h.UserId == 1 && h.IsEnabled) ?? throw new ValidationException("Habit not found");
				});
			_habitRepositoryMock.Setup(x => x.Archive(It.IsAny<Habit>()))
				.Callback<Habit>(h => h.IsEnabled = false)
				.Returns(Task.CompletedTask);
			_habitRepositoryMock.Setup(x => x.GetPaginatedAsync(1, It.IsAny<PaginationQuery>()))
				.ReturnsAsync(() =>
				{
					var activeHabits = habits.Where(h => h.UserId == 1 && h.IsEnabled).ToList();
					return new PagedResult<Habit>
					{
						PageNumber = pagination.PageNumber,
						PageSize = pagination.PageSize,
						TotalItems = activeHabits.Count,
						Items = activeHabits
					};
				});

			// Act
			await service.Archive(1, 40);
			var result = await service.Get(pagination, 1);

			// Assert
			Assert.Empty(result.Items);
			Assert.Equal(0, result.TotalItems);
		}
	}
}
