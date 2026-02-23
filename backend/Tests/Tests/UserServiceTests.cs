using Configuration.ExceptionHandle;
using Dtos.Request.User;
using Models.User;
using Moq;
using Repositories.Interfaces;
using Services.Services;

namespace Tests.Tests
{
	public class UserServiceTests
	{
		private Mock<IUserRepository> _userRepositoryMock;

		public UserServiceTests()
		{
			_userRepositoryMock = new Mock<IUserRepository>();
		}

		[Fact(DisplayName = "Should Create When Email Not Exists")]
		public async Task Should_Create_User_When_Email_Not_Exists() 
		{
			// Arrange
			var service = new UserService(_userRepositoryMock.Object);

			_userRepositoryMock.Setup(x => x.ExistsUserByEmailAsync(It.IsAny<string>())).ReturnsAsync(false);

			var request = new UserCreateRequest
			{
				Name = "name",
				Email = "email@mail.com",
				Password = "123456"
			};

			// Act
			var result = await service.Create(request);

			// Assert
			Assert.Equal("name", request.Name);
			_userRepositoryMock.Verify(x => x.Add(It.IsAny<User>()), Times.Once);
		}

		[Fact(DisplayName = "Should Fail When User Already Exists")]
		public async Task Should_Fail_When_User_Already_Exists()
		{
			// Arrange
			var service = new UserService(_userRepositoryMock.Object);

			_userRepositoryMock.Setup(x => x.ExistsUserByEmailAsync(It.IsAny<string>())).ReturnsAsync(true);

			var request = new UserCreateRequest
			{
				Name = "name",
				Email = "email@mail.com",
				Password = "123456"
			};

			// Act & Assert
			var exception = await Assert.ThrowsAsync<ValidationException>(() => service.Create(request));
			
			Assert.Equal("UserAlreadyRegistered", exception.Message);
		}
	}
}
