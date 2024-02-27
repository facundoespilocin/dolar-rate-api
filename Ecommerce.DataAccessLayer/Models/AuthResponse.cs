using Ecommerce.DataAccessLayer.Dtos.User;

namespace Ecommerce.DataAccessLayer.Models
{
    public class AuthResponse : AuthenticatedUser
    {
        public AuthResponse()
        {
            UserData = new UserDataDTO();
        }

        public string Token { get; set; }

        public UserDataDTO UserData
        {
            get; set;
        }

        public RefreshToken RefreshToken { get; set; }
    }

    public class AuthenticatedUser
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        //public long DefaultCompanyId { get; set; }
    }

    public class RefreshToken
    {
        public long UserId { get; set; } = 0;
        public string Token { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Expires { get; set; }
    }
}