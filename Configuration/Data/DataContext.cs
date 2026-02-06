using Microsoft.EntityFrameworkCore;
using Models.Habit;
using Models.User;

namespace Configuration.Data
{
	public class DataContext: DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{ }
		
		public DbSet<User> Users { get; set; }
		public DbSet<Habit> Habits { get; set; }
		public DbSet<HabitCheckIn> HabitCheckIns { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			
			modelBuilder.Entity<HabitCheckIn>() .HasIndex(x => new { x.HabitId, x.UserId, x.Date }) .IsUnique();
		}
	}
}
