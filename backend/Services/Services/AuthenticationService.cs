using Dtos.Request.Login;
using Dtos.Response.Token;
using Repositories.Interfaces;
using Services.Interface;
using Services.Validator;

namespace Services.Services
{
	public class AuthenticationService(ITokenService tokenService,
									   IUserRepository userRepository) : IAuthenticationService
	{
		public async Task<TokenResponse> Authentication(LoginRequest loginRequest)
		{
			if (loginRequest == null)
				throw new ArgumentException("error");

			var user = await userRepository.GetByEmailAsync(loginRequest.Email);

			var isLoginValid = AuthenticationValidator.LoginValidation(loginRequest, user.Password);

			if (!isLoginValid)
				throw new ArgumentException("error");

			var token = tokenService.GenerateToken(user);

			return new TokenResponse
			{
				Token = token.Token,
				ExpiresIn = token.ExpiresIn
			};
		}
	}
}
