using Dtos.Request.Login;

namespace Services.Validator
{
	public static class AuthenticationValidator
	{
		public static bool LoginValidation(LoginRequest loginRequest, string password)
		{
			if (string.IsNullOrEmpty(loginRequest.Email))
				return false;

			if (string.IsNullOrEmpty(loginRequest.Password))
				return false;

			var isPasswordValid = BCrypt.Net.BCrypt.Verify(loginRequest.Password, password);

			if (!isPasswordValid)
				return false;

			return isPasswordValid;
		}
	}
}
