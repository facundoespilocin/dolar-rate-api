using Ecommerce.DataAccessLayer.Models;

namespace Ecommerce.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> Authenticate(AuthRequest request);
        Task<AuthResponse> RefreshToken(string refreshToken);
    }
}