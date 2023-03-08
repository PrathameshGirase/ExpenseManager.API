using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseManager.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
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
        }
    }
}
