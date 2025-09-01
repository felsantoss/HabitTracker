using Dtos.Response.Token;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.User;
using Services.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services.Services
{
	public class TokenService(IConfiguration configuration) : ITokenService
	{
		private readonly string _secretKey = configuration["Jwt:SecretKey"] ?? string.Empty;

		public TokenResponse GenerateToken(User user)
		{
			var handler = new JwtSecurityTokenHandler();

			var key = Encoding.UTF8.GetBytes(_secretKey);

			var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = GenerateClaims(user),
				SigningCredentials = credentials,
				Expires = DateTime.UtcNow.AddMinutes(60)
			};

			var securityToken = handler.CreateToken(tokenDescriptor);

			var token = handler.WriteToken(securityToken);

			return new TokenResponse
			{
				Token = token,
				ExpiresIn = 60 // TODO achar forma de colocar o valor aqui
			};
		}

		private static ClaimsIdentity GenerateClaims(User user)
		{
			var claims = new ClaimsIdentity();

			claims.AddClaim(new Claim(ClaimTypes.Name, user.Id.ToString()));

			return claims;
		}
	}
}
