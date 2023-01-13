using Microsoft.EntityFrameworkCore;
using Net6WebApi.Entity;

namespace Net6WebApi.Config
{
    public class SqlServerContext : DbContext
    {
		public DbSet<Book> Book { get; set; }
		public SqlServerContext(DbContextOptions<SqlServerContext> options) : base(options)
		{
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
		}
	}
}
