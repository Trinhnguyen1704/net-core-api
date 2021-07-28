using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace net_core_api.Models
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<BookContext> options)
            : base(options)
        {
        }
        public DbSet<Book> Books {get; set;}
        public DbSet<Category> Categories {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        // configures one-to-many relationship
            modelBuilder.Entity<Category>()
            .HasMany(c => c.Books)
            .WithOne(b => b.CategoryIdNavigation)
            .HasForeignKey(b => b.CategoryId);
        }
    }
}
