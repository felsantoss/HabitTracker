using Microsoft.EntityFrameworkCore;
using Models.User;

namespace Configuration.Data
{
	public class DataContext(DbContextOptions<DataContext> options) : DbContext
	{
		public DbSet<User> Users { get; set; }
	}
}
