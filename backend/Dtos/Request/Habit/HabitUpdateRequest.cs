using System.Diagnostics.CodeAnalysis;

namespace Dtos.Request.Habit
{
	[ExcludeFromCodeCoverage]
	public class HabitUpdateRequest
	{
		public required string Title { get; set; }
		public string Description { get; set; } = string.Empty;
	}
}
