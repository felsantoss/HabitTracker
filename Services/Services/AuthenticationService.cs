using Dtos.Request.Login;
using Dtos.Response.Token;
using Repositories.Interfaces;
using Services.Interface;

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
				throw new ArgumentNullException(nameof(loginRequest));


		}
	}
}
