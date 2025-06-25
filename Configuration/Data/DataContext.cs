using Microsoft.EntityFrameworkCore;
using Models.User;

namespace Configuration.Data
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options) {}

		public DbSet<User> Users { get; set; }
	}
}
