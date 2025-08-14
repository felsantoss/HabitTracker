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

			_userRepositoryMock.Setup(x => x.ExistsUserByEmailAsync(It.IsAny<string>()))
							   .ReturnsAsync(false);

			var service = new UserService(_userRepositoryMock.Object);
		}
	}
}
