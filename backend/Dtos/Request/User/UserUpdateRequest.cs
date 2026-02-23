using System.Diagnostics.CodeAnalysis;

namespace Dtos.Request.User
{
	[ExcludeFromCodeCoverage]
	public class UserUpdateRequest
	{
		public string Name { get; set; } = string.Empty;
	}
}
