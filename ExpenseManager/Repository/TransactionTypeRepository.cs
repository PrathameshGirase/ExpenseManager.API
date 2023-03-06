using ExpenseManager.Contracts;
using ExpenseManager.Data;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManager.Repository
{
    public class TransactionTypeRepository : GenericRepository<TransactionType>, ITransactionTypeRepository
    {
        private readonly ExpenseManagerDbContext context;
        public TransactionTypeRepository(ExpenseManagerDbContext context) : base(context)
        {
            this.context = context;
        }


        public async Task<TransactionType> GetDetails(int id)
        {
            return await context.TransactionTypes.Include(q => q.Transactions).FirstOrDefaultAsync(q => q.Id == id);
        }
    }
}
