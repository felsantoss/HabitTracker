using System.Diagnostics.CodeAnalysis;

namespace Dtos.Response.User
{
	[ExcludeFromCodeCoverage]
	public class UserCreateResponse
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
	}
}
