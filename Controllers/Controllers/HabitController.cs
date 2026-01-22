using Dtos.Request.Habit;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;
using System.Security.Claims;

namespace Api.Controllers
{
	[ApiController]
	[Route("api/v1/habit")]
	[Produces("application/json")]
	public class HabitController(IHabitService habitService) : ControllerBase
	{
		[HttpPost]
		public async Task<IActionResult> Create(HabitCreateRequest habitCreateRequest)
		{
			var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId)) 
				return Unauthorized("Não foi possível identificar o usuário.");

			var response = await habitService.Create(habitCreateRequest, userId);

			return new ObjectResult(response);
		}
	}
}
