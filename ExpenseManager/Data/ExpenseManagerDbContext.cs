using Microsoft.EntityFrameworkCore;

namespace ExpenseManager.Data
{
    public class ExpenseManagerDbContext : DbContext
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
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Food And Beverages",

                },
                new Category
                {
                    Id = 2,
                    Name = "Pantry",

                },
                new Category
                {
                    Id = 3,
                    Name = "Stationary",

                },
                new Category
                {
                    Id = 4,
                    Name = "Travel",

                },
                new Category
                {
                    Id = 5,
                    Name = "Staff",

                },
                new Category
                {
                    Id = 6,
                    Name = "Others",

                }

            );

            modelBuilder.Entity<TransactionType>().HasData(
                new TransactionType
                {
                    Id = 1,
                    Name = "Expense"
                },
                new TransactionType
                {
                    Id = 2,
                    Name = "Income"
                }
                );

            modelBuilder.Entity<Transaction>().HasData(
                new Transaction
                {
                    Id = 1,
                    Amount = 50,
                    Name = "Starbucks",
                    Description = "My Treat",
                    Date = DateTime.Now,
                    TransactionTypeId= 1,
                    CategoryId= 1,

                },
                new Transaction
                {
                    Id = 2,
                    Amount = 100,
                    Name = "Stocks",
                    Description = "Profit",
                    Date = DateTime.Now,
                    TransactionTypeId = 2,
                    CategoryId = 5,
                },
                new Transaction
                {
                    Id = 5,
                    Amount = 100,
                    Name = "Salary",
                    Description = "Null",
                    Date = DateTime.Now,
                    TransactionTypeId = 2,
                    CategoryId = 5,
                }
                );
        }
    }
}
