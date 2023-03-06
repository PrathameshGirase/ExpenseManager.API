using ExpenseManager.Data;

namespace ExpenseManager.Contracts
{
    public interface ITransactionTypeRepository : IGenericRepository<TransactionType>
    {
        Task<TransactionType> GetDetails(int id);
    }
}
