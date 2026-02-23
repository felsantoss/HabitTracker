namespace Services.Security
{
	public static class SecurityHandler
	{
		public static string GenerateEncryptedPassword(string password)
		{
			int workFactor = 10;

			var salt = BCrypt.Net.BCrypt.GenerateSalt(workFactor);

			return BCrypt.Net.BCrypt.HashPassword(password, salt);
		}

		public static bool VerifyPassword(string password, string hashedPassword)
		{
			return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
		}
	}
}
