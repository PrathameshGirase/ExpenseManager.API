namespace ExpenseManager.Models.Users
{
    public class AuthResponseDto
    {
        public String UserId { get; set; }
        public String Token { get; set; }

        public String RefreshToken{ get; set; }
    }
}
