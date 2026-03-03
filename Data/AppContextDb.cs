using Microsoft.EntityFrameworkCore;
using SafeBanking.Models;

namespace SafeBanking.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(
                "Host=localhost;Port=5432;Database=SafeBankingDb;Username=postgres;Password=postgres"
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasIndex(a => a.AccountNumber)
                .IsUnique();
        }
    }
}