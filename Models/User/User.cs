using System.Diagnostics.CodeAnalysis;

namespace Models.User
{
	[ExcludeFromCodeCoverage]
	public class User
	{
		public int Id { get; set; }
		public required string Name { get; set; }
		public required string Email { get; set; }
		public required string Password { get; set; }
	}
}
