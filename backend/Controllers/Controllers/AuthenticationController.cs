using Dtos.Request.Login;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;

namespace Api.Controllers
{
	[ApiController]
	[Route("api/v1/authentication")]
	[Produces("application/json")]
	public class AuthenticationController
	{
		private readonly IAuthenticationService _authenticationService;

		public AuthenticationController(IAuthenticationService authenticationService)
		{
			_authenticationService = authenticationService;
		}

		[HttpPost("login")]
		public async Task<IActionResult> Authentication(LoginRequest loginRequest)
		{
			var response = await _authenticationService.Authentication(loginRequest);

			return new ObjectResult(response);
		}
	}
}
