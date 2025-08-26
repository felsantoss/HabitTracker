﻿using System.Diagnostics.CodeAnalysis;

namespace Models.Habit
{
	[ExcludeFromCodeCoverage]
	public class Habit
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public DateTime StartDate { get; set; }
		public bool IsEnabled { get; set; }
		public required Models.User.User User { get; set; }
	}
}
