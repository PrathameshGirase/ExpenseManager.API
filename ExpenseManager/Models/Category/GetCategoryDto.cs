using ExpenseManager.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseManager.Models.Category
{
    public class GetCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }


    
}
