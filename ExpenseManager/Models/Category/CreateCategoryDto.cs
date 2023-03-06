using Microsoft.Build.Framework;

namespace ExpenseManager.Models.Category
{
    public class CreateCategoryDto
    {
        [Required]
        public string Name { get; set; }

    }
}
 