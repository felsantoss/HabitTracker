using Configuration.ExceptionHandle;
using Dtos.Request.Habit;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;
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
			var userId = HttpContext.GetAuthenticatedUserId();
			
			var response = await habitService.Get(request, userId);
			
			return new ObjectResult(response);
		}
		
		[HttpPost]
		[Authorize]
		[ProducesResponseType(typeof(HabitResponse), StatusCodes.Status200OK)]
		public async Task<IActionResult> Create(HabitCreateRequest habitCreateRequest)
		{
			var userId = HttpContext.GetAuthenticatedUserId();

			var response = await habitService.Create(habitCreateRequest, userId);

			return new ObjectResult(response);
		}

		[HttpPut("{habitId}")]
		[Authorize]
		[ProducesResponseType(typeof(HabitResponse), StatusCodes.Status200OK)]
		public async Task<IActionResult> Update(int habitId, HabitUpdateRequest habitUpdateRequest)
		{
			var userId = HttpContext.GetAuthenticatedUserId();

			var response = await habitService.Update(userId, habitId, habitUpdateRequest);

			return Ok(response);
		}

		[HttpDelete("{habitId}")]
		[Authorize]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<IActionResult> Archive(int habitId)
		{
			var userId = HttpContext.GetAuthenticatedUserId();

			await habitService.Archive(userId, habitId);

			return NoContent();
		}


		[HttpGet("{habitId}/checkins")]
		[Authorize]
		public async Task<IActionResult> GetCheckIn(int habitId, [FromQuery] PaginationQuery pagination)
		{
			var userId = HttpContext.GetAuthenticatedUserId();

			var response = await habitService.GetCheckIn(userId, habitId, pagination);

			return Ok(response);
		}
		
		[HttpPost("{habitId}/checkin")]
		[Authorize]
		public async Task<IActionResult> CheckIn(int habitId)
		{
			var userId = HttpContext.GetAuthenticatedUserId();

			var response = await habitService.CreateCheckIn(userId, habitId);
			
			return new ObjectResult(response);
		}
	}
}
