using System.ComponentModel.DataAnnotations;

namespace Ecommerce.DataAccessLayer.Models
{
    public class ForgotPasswordRequest
    {
        [Required]
        public string Email { get; set; }
    }
}