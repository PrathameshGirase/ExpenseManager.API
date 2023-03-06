using System.ComponentModel.DataAnnotations;

namespace ExpenseManager.Models.Transaction
{
    public abstract class TransactionBaseDto
    {
        [Microsoft.Build.Framework.Required]
        public string Name { get; set; }

        [Microsoft.Build.Framework.Required]
        public string Description { get; set; }

        [Microsoft.Build.Framework.Required]
        [Range(1,int.MaxValue)]
        public double Amount { get; set; }

        [Microsoft.Build.Framework.Required]
        public DateTime Date { get; set; }

        [Microsoft.Build.Framework.Required]
        [Range(1, int.MaxValue)]
        public int TransactionTypeId { get; set; }

        [Microsoft.Build.Framework.Required]
        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; }
    }
}
