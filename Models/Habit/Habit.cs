using System.Diagnostics.CodeAnalysis;

namespace Models.Habit
{
	[ExcludeFromCodeCoverage]
	public class Habit
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public DateTime StartDate { get; set; }
		public bool IsEnabled { get; set; }
		public User.User User { get; set; }
	}
}
