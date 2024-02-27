namespace Ecommerce.DataAccessLayer.Models
{
    public class ValidateResetPasswordTokenResponse
    {
        public bool IsValid { get; set; }
        public bool IsUsed { get; set; }
        public string? Email { get; set; }
    }
}