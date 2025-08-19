using Dtos.Request.Login;
using Dtos.Response.Token;
using Models.User;
using Moq;
using Repositories.Interfaces;
using Services.Interface;
using Services.Services;

namespace Tests.Tests
{
	public class AuthenticationServiceTests
	{
		private Mock<IUserRepository> _userRepositoryMock;
		private Mock<ITokenService> _tokenServiceMock;

		public AuthenticationServiceTests()
		{
			_userRepositoryMock = new Mock<IUserRepository>();
			_tokenServiceMock = new Mock<ITokenService>();
		}


		[Fact(DisplayName = "Should Allow Login When Credencials Is Ok")]
		public async Task Should_Allow_Login_When_Credencials_Is_Ok()
		{
			// Arrange 
			var service = new AuthenticationService(_tokenServiceMock.Object, _userRepositoryMock.Object);

			var encryptedPassword = BCrypt.Net.BCrypt.HashPassword("123456");

			_userRepositoryMock.Setup(x => x.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(new User { Email = "email@mail.com", Name = "name", Password = encryptedPassword });
			_tokenServiceMock.Setup(x => x.GenerateToken(It.IsAny<User>())).Returns(new Dtos.Response.Token.TokenResponse { Token = Guid.NewGuid().ToString(), ExpiresIn = 60 });

			var request = new LoginRequest
			{
				Email = "email@mail.com",
				Password = "123456"
			};

			// Act
			var response = await service.Authentication(request);

			// Assert
			Assert.NotNull(response);
			Assert.False(string.IsNullOrEmpty(response.Token));
			Assert.Equal(60, response.ExpiresIn);
		}
	}
}
