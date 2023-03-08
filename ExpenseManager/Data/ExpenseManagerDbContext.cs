using ExpenseManager.Data.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManager.Data
{
    public class ExpenseManagerDbContext : IdentityDbContext<ApiUser>
    {

        public ExpenseManagerDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<TransactionType> TransactionTypes { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
        }
    }
}
