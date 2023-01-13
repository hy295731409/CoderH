using ConsoleAppNet6.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppNet6.Mapping
{
    /// <summary>
    /// 实体与数据的映射关系
    /// </summary>
    public class BookMapping : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("t_book");
            builder.HasKey(x => x.Id);
            builder.Property(b=>b.Name).HasMaxLength(50).IsRequired(true);
        }
    }
}
