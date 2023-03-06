using ExpenseManager.Models.Transaction;

namespace ExpenseManager.Models.Transaction_Type
{
    public class TransactionTypeDto
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public List<TransactionDto> Transactions { get; set; }
    }
}
