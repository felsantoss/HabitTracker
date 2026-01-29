using Dtos.Request.Habit;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;
using System.Security.Claims;
using Dtos.Pagination;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
	[ApiController]
	[Route("api/v1/habit")]
	[Produces("application/json")]
	public class HabitController(IHabitService habitService) : ControllerBase
	{
		
		[HttpGet]
		[Authorize]
		public async Task<IActionResult> Get([FromQuery] PaginationQuery request)
		{
			var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			
			if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId)) 
				return Unauthorized("Não foi possível identificar o usuário.");
			
			var response = await habitService.Get(request, userId);
			
			return new ObjectResult(response);
		}
		
		[HttpPost]
		[Authorize]
		public async Task<IActionResult> Create(HabitCreateRequest habitCreateRequest)
		{
			var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId)) 
				return Unauthorized("Não foi possível identificar o usuário.");

			var response = await habitService.Create(habitCreateRequest, userId);

			return new ObjectResult(response);
		}

		[HttpPost("{habitId}/checkin")]
		[Authorize]
		public async Task<IActionResult> CheckIn(int habitId)
		{
			var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			
			if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId)) 
				return Unauthorized("Não foi possível identificar o usuário.");

			var response = await habitService.CheckIn(userId, habitId);
			
			return new ObjectResult(response);
		}
	}
}
