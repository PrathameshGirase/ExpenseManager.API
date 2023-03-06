using ExpenseManager.Data;

namespace ExpenseManager.Contracts
{
    public interface ICategoriesRepository : IGenericRepository<Category>
    {
        Task<Category> GetDetails(int id);
    }
}
