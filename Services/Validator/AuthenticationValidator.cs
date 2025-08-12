using BCrypt.Net;
using Dtos.Request.Login;
using Repositories.Interfaces;

namespace Services.Validator
{
	public class AuthenticationValidator
	{
		private readonly IUserRepository _userRepository;

		public AuthenticationValidator(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task<bool> LoginValidation(LoginRequest loginRequest)
		{
			if (string.IsNullOrEmpty(loginRequest.Email))
				throw new ArgumentException("error");

			if (string.IsNullOrEmpty(loginRequest.Password))
				throw new ArgumentException("error");

			var user = await _userRepository.GetByEmailAsync(loginRequest.Email);

			var isPasswordValid = BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password);

			if (!isPasswordValid)
				throw new ArgumentException("error");

			return isPasswordValid;
		}
	}
}
