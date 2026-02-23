using Dtos.Request.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;

namespace Api.Controllers
{
	[ApiController]
	[Route("v1/api/user")]
	[Produces("application/json")]
	public class UserController(IUserService userService) : ControllerBase
	{
		[HttpPost]
		public async Task<IActionResult> Create(UserCreateRequest userCreateRequest)
		{
			var response = await userService.Create(userCreateRequest);

			return new ObjectResult(response);
		}

		[HttpPut("{id}")]
		[Authorize]
		public async Task<IActionResult> Update(int id, UserUpdateRequest userUpdateRequest)
		{
			var response = await userService.Update(id, userUpdateRequest);

			return new ObjectResult(response);
		}
	}
}
