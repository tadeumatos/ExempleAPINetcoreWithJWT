using Bookstore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookstore.Infrastructure.Data.EntityConfiguration
{
    public class AuthorEntityConfiguration : IEntityTypeConfiguration<AuthorEntity>
    {
        public void Configure(EntityTypeBuilder<AuthorEntity> builder)
        {
            builder.HasKey(a => a.AuthorId);
            builder.Property(a => a.Name)
                   .HasColumnType("varchar")
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(a => a.DateIncluded)
                   .HasColumnType("datetime")
                   .IsRequired()
                   .HasDefaultValueSql("GetDate()");
            builder.Property(a => a.DateUpdated)
                   .HasColumnType("datetime");
                   
        }
    }
}
