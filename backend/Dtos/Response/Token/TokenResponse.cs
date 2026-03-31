using System.Diagnostics.CodeAnalysis;

namespace Dtos.Response.Token
{
	[ExcludeFromCodeCoverage]
	public class TokenResponse
	{
		public string Token { get; set; } = string.Empty;
		public int ExpiresIn { get; set; }
	}
}
