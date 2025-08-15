using Dtos.Request.User;
using Models.User;
using Moq;
using Repositories.Interfaces;
using Services.Services;
using System.Threading.Tasks;


namespace Tests.Tests
{
	public class UserServiceTests
	{
		private Mock<IUserRepository> _userRepositoryMock;

		public UserServiceTests()
		{
			_userRepositoryMock = new Mock<IUserRepository>();

			_userRepositoryMock.Setup(x => x.ExistsUserByEmailAsync(It.IsAny<string>()))
							   .ReturnsAsync(false);
		}

		

		[Fact(DisplayName = "Should Create When Email Not Exists")]
		public async Task Should_Create_User_When_Email_Not_Exists() 
		{
			// Act
			var service = new UserService(_userRepositoryMock.Object);

			var request = new UserCreateRequest
			{
				Name = "name",
				Email = "email@mail.com",
				Password = "123456"
			};

			// Arrange
			var result = await service.Create(request);

			// Assert
			Assert.Equal("name", request.Name);
			_userRepositoryMock.Verify(x => x.Add(It.IsAny<User>()), Times.Once);
		}
	}
}
