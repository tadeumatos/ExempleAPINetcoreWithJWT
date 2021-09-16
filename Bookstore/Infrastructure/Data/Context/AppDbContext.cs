using Bookstore.Infrastructure.Data.EntityConfiguration;
using Bookstore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Infrastructure.Data.Context
{
    public class AppDbContext:IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {}

        public DbSet<BookEntity> Books { get; set; }
        public DbSet<AuthorEntity> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

           // builder.Entity<BookEntity>()
           //.HasOne(b => b.Author)
           //.WithMany(a => a.Books)
           //.OnDelete(DeleteBehavior.SetNull);


            builder.ApplyConfiguration(new AuthorEntityConfiguration());
            builder.ApplyConfiguration(new BookEntityConfiguration());
        }
    }
}
