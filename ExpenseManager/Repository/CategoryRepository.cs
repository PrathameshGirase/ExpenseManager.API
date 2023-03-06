using ExpenseManager.Contracts;
using ExpenseManager.Data;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManager.Repository
{
    public class CategoryRepository : GenericRepository<Category>, ICategoriesRepository
    {
        private readonly ExpenseManagerDbContext context;

        public CategoryRepository(ExpenseManagerDbContext context) : base(context)
        {
            this.context = context;
        }

       

        public async Task<Category> GetDetails(int id)
        {
            return await context.Categories.Include(q => q.Transactions)
                   .FirstOrDefaultAsync(q => q.Id == id);
        }
    }
}
