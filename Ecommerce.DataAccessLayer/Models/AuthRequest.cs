using System.ComponentModel.DataAnnotations;

namespace Ecommerce.DataAccessLayer.Models
{
    public class AuthRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}