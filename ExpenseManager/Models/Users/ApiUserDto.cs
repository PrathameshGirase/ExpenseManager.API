using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace ExpenseManager.Models.Users
{
    public class ApiUserDto
    {
        [Microsoft.Build.Framework.Required]
        public String FirstName { get; set; }
        [Microsoft.Build.Framework.Required]
        public String LastName { get; set; }
        [Microsoft.Build.Framework.Required]
        [EmailAddress]
        public String Email { get; set; }
        [Microsoft.Build.Framework.Required]
        [StringLength(15,ErrorMessage = "Your Password is Limited to {2} to {1} characters",MinimumLength = 6)]
        public String Password { get; set; }

    }
}
