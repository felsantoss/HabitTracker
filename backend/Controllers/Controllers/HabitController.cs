using Dtos.Request.Habit;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;
using System.Security.Claims;
using Dtos.Pagination;
using Microsoft.AspNetCore.Authorization;
using Dtos.Response.Habit;

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
		[ProducesResponseType(typeof(HabitResponse), StatusCodes.Status200OK)]
		public async Task<IActionResult> Create(HabitCreateRequest habitCreateRequest)
		{
			var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId)) 
				return Unauthorized("Não foi possível identificar o usuário.");

			var response = await habitService.Create(habitCreateRequest, userId);

			return new ObjectResult(response);
		}

		[HttpPut("{habitId}")]
		[Authorize]
		[ProducesResponseType(typeof(HabitResponse), StatusCodes.Status200OK)]
		public async Task<IActionResult> Update(int habitId, HabitUpdateRequest habitUpdateRequest)
		{
			var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
				return Unauthorized("Não foi possível identificar o usuário.");

			var response = await habitService.Update(userId, habitId, habitUpdateRequest);

			return Ok(response);
		}

		[HttpDelete("{habitId}")]
		[Authorize]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<IActionResult> Archive(int habitId)
		{
			var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
				return Unauthorized("Não foi possível identificar o usuário.");

			await habitService.Archive(userId, habitId);

			return NoContent();
		}


		[HttpGet("{habitId}/checkins")]
		[Authorize]
		public async Task<IActionResult> GetCheckIn(int habitId, [FromQuery] PaginationQuery pagination)
		{
			var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
				return Unauthorized("Não foi possível identificar o usuário.");

			var response = await habitService.GetCheckIn(userId, habitId, pagination);

			return Ok(response);
		}
		
		[HttpPost("{habitId}/checkin")]
		[Authorize]
		public async Task<IActionResult> CheckIn(int habitId)
		{
			var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			
			if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId)) 
				return Unauthorized("Não foi possível identificar o usuário.");

			var response = await habitService.CreateCheckIn(userId, habitId);
			
			return new ObjectResult(response);
		}
	}
}
