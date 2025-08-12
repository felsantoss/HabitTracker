using Dtos.Request.Login;
using Dtos.Response.Token;
using Repositories.Interfaces;
using Services.Interface;
using Services.Validator;

namespace Services.Services
{
	public class AuthenticationService : IAuthenticationService
	{
		private readonly ITokenService _tokenService;
		private readonly IUserRepository _userRepository;

		public AuthenticationService(ITokenService tokenService, 
									 IUserRepository userRepository)
		{
			_tokenService = tokenService;
			_userRepository = userRepository;
		}

		public async Task<TokenResponse> Authentication(LoginRequest loginRequest)
		{
			if (loginRequest == null)
				throw new ArgumentException("error");

			var user = await _userRepository.GetByEmailAsync(loginRequest.Email);

			var isLoginValid = AuthenticationValidator.LoginValidation(loginRequest, user.Password);

			if (!isLoginValid)
				throw new ArgumentException("error");

			var token = _tokenService.GenerateToken(user);

			return new TokenResponse
			{
				Token = token.Token,
				ExpiresIn = token.ExpiresIn
			};
		}
	}
}
