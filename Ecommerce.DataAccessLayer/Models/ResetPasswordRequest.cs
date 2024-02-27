using Ecommerce.Utils;

namespace Ecommerce.DataAccessLayer.Models
{
    public class RecoveryEmailRequest
    {
        public Enums.TokenTypes TokenType { get; set; } = Enums.TokenTypes.ForgotToken;
        public string? Token { get; set; }
    }

    public class ResetPasswordRequest
    {
        public string? Token { get; set; }
        public string? NewPassword { get; set; }
    }

    public class ResetCurrentPasswordRequest
    {
        public long Id { get; set; }
        public string? NewPassword { get; set; }
    }
}