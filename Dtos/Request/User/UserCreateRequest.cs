﻿using System.Diagnostics.CodeAnalysis;

namespace Dtos.Request.User
{
	[ExcludeFromCodeCoverage]
	public class UserCreateRequest
	{
		public string Name { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
	}
}
