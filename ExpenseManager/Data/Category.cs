namespace ExpenseManager.Data
{
    public class Category
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual IList<Transaction> Transactions { get; set;}
    }
}