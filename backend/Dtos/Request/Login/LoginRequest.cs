using System.Diagnostics.CodeAnalysis;

namespace Dtos.Request.Login
{
	[ExcludeFromCodeCoverage]
	public class LoginRequest
	{
		public string Email { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
	}
}
