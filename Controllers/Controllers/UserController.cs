using Dtos.Request.User;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;

namespace Api.Controllers
{
	[ApiController]
	[Route("/api/[controller]")]
	[Produces("application/json")]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpPost]
		public async Task<IActionResult> Create(UserCreateRequest userCreateRequest)
		{
			var response = await _userService.Create(userCreateRequest);

			return new ObjectResult(response);
		}
	}
}
