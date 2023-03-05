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

        public DbSet<Transaction_Type> TransactionTypes { get; set; }
        System.TimeSpan duration = new System.TimeSpan(30, 0, 0, 0);
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
                    Name = "Others",

                }

            );

            modelBuilder.Entity<Transaction_Type>().HasData(
                new Transaction_Type
                {
                    Id = 1,
                    Name = "Expense"
                },
                new Transaction_Type
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
                    Date = DateTime.Now.Add(duration),
                    Transaction_Type_Id= 1,
                    CategoryId= 1,

                },
                new Transaction
                {
                    Id = 2,
                    Amount = 100,
                    Name = "Stocks",
                    Description = "Profit",
                    Date = DateTime.Now.Add(duration),
                    Transaction_Type_Id = 2,
                    CategoryId = 5,

                }
                );
        }
    }
}
