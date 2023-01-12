using Microsoft.EntityFrameworkCore;

namespace Net6WebApi.Config
{
    public class SqlServerContext : DbContext
    {
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
