using System.Diagnostics.CodeAnalysis;

namespace Dtos.Request.Habit
{

	[ExcludeFromCodeCoverage]
	public class HabitCreateRequest
	{
		public required string Name { get; set; }
		public string Description { get; set; } = string.Empty;
		public DateTime? StartDate { get; set; }
	}
}
