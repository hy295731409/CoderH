using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Net6WebApi.Entity;

namespace Net6WebApi.Mapping
{
    public class BookMapping : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("t_book");
            builder.HasKey(x => x.Id);
            builder.Property(b => b.Name).HasMaxLength(50).IsRequired(true);
        }
    }
}
