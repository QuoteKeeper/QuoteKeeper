using Microsoft.EntityFrameworkCore;
using QuoteKeeper.API.Models;

namespace QuoteKeeper.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<UserFavoriteQuote> UserFavoriteQuotes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>()
        .HasOne(b => b.User)
        .WithMany(u => u.Books)
        .HasForeignKey(b => b.UserId)
        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserFavoriteQuote>()
                .HasKey(ufq => new { ufq.UserId, ufq.QuoteId });

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
