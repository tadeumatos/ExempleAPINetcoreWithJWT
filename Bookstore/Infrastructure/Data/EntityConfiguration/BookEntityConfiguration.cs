using Bookstore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookstore.Infrastructure.Data.EntityConfiguration
{
    public class BookEntityConfiguration : IEntityTypeConfiguration<BookEntity>
    {
        public void Configure(EntityTypeBuilder<BookEntity> builder)
        {
            builder.HasKey(b => b.BookId);
            builder.Property(b => b.Name)
                .HasColumnType("varchar")
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(b => b.DateIncluded)
                .HasColumnType("datetime")
                .IsRequired()
                .HasDefaultValueSql("GetDate()");
            builder.Property(b => b.DateUpdated)
                .HasColumnType("datetime");
                
            
        }
    }
}
