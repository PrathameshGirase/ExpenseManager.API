using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseManager.Data.Configurations
{
    public class TransactionTypeConfiguration : IEntityTypeConfiguration<TransactionType>
    {
        public void Configure(EntityTypeBuilder<TransactionType> builder)
        {
            builder.HasData(
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
        }
    }
}