﻿namespace Dtos.Response.Token
{
	public class TokenResponse
	{
		public string Token { get; set; } = string.Empty;
		public int ExpiresIn { get; set; }
	}
}
