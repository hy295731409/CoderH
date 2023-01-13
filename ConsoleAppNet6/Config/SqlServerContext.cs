using ConsoleAppNet6.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppNet6.Config
{
    public class SqlServerContext : DbContext
    {
        private IConfiguration configuration;

        public DbSet<Book> Book { get; set; }

        #region 方式1：配置链接字符串
        public SqlServerContext()
        {
            configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
        }
        /// <summary>
        /// 配置连接信息（这里配置数据库链接字符串不是最佳做法）
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var conStr = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(conStr);
        }
        #endregion

        #region 方式2：在启动时配置
        /// <summary>
        /// 通过传入配置来获取context
        /// </summary>
        /// <param name="contextOptions"></param>
        public SqlServerContext(DbContextOptions<SqlServerContext> contextOptions) : base(contextOptions)
        {

        }
        #endregion

        /// <summary>
        /// 加载所有实现接口IEntityTypeConfiguration的类
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
