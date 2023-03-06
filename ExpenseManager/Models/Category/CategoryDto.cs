using ExpenseManager.Models.Transaction;

namespace ExpenseManager.Models.Category
{
    public class CategoryDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<TransactionDto> Transactions { get; set; }

    }


    
}
