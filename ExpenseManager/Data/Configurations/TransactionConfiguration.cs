using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseManager.Data.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasData(
                new Transaction
                {
                    Id = 1,
                    Amount = 50,
                    Name = "Starbucks",
                    Description = "My Treat",
                    Date = DateTime.Now.ToString(),
                    TransactionTypeId = 1,
                    CategoryId = 1,

                },
                new Transaction
                {
                    Id = 2,
                    Amount = 100,
                    Name = "Stocks",
                    Description = "Profit",
                    Date = DateTime.Now.ToString(),
                    TransactionTypeId = 2,
                    CategoryId = 5,
                },
                new Transaction
                {
                    Id = 5,
                    Amount = 100,
                    Name = "Salary",
                    Description = "Null",
                    Date = DateTime.Now.ToString(),
                    TransactionTypeId = 2,
                    CategoryId = 5,
                }
                );
        }
    }
}