using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseManager.Data
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public double Amount { get; set; }

        public DateTime Date { get; set; }

        [ForeignKey(nameof(TransactionTypeId))]
        public int TransactionTypeId { get; set; }

        public TransactionType TransactionType { get; set; }

        
        [ForeignKey(nameof(CategoryId))]
        public int CategoryId { get; set; } 

        public Category Category { get; set; }


    }
}
