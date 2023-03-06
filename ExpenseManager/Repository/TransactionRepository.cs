using ExpenseManager.Contracts;
using ExpenseManager.Data;

namespace ExpenseManager.Repository
{
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(ExpenseManagerDbContext context) : base(context)
        {
        }
    }
}
