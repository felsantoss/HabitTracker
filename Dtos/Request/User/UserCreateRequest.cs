using System.Diagnostics.CodeAnalysis;

namespace Dtos.Request.User
{
	[ExcludeFromCodeCoverage]
	public class UserCreateRequest
	{
		public required string Name { get; set; }
		public required string Email { get; set; }
		public required string Password { get; set; }
	}
}
