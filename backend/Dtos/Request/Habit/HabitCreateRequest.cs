using System.Diagnostics.CodeAnalysis;

namespace Dtos.Request.Habit
{

	[ExcludeFromCodeCoverage]
	public class HabitCreateRequest
	{
		public required string Title { get; set; }
		public string Description { get; set; }
		public DateTime? Date { get; set; }	
	}
}
