using System.Diagnostics.CodeAnalysis;

namespace Dtos.Response.Habit
{
	[ExcludeFromCodeCoverage]
	public class HabitCreateResponse
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public DateTime StartDate { get; set; }
	}
}
