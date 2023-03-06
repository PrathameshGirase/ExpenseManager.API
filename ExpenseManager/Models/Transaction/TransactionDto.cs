namespace ExpenseManager.Models.Transaction
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public double Amount { get; set; }

        public DateTime Date { get; set; }
        public int TransactionTypeId { get; set; }
        public int CategoryId { get; set; }

    }
}
