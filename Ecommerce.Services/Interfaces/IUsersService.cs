using Ecommerce.DataAccessLayer.Dtos.User;
using Ecommerce.DataAccessLayer.Entities.Customer;
using Ecommerce.DataAccessLayer.Entities.User;
using Ecommerce.DataAccessLayer.Models;

namespace Ecommerce.Services.Interfaces
{
    public interface IUsersService
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(long userId);
        Task<CustomerDataDTO> GetCustomerByEmail(string email);
        Task<ServiceResponse> Create(CreateUserRequest user);
        Task UpdateUser(User userRequest);
        Task<ResetPasswordResponse> UpdateUserPassword(ResetPasswordRequest request);
        Task<bool> ValidateResetPasswordToken(string token);
        Task PostForgotPassword(string email);
        Task<UserDataDTO> GetUserDataById(long userId);
        Task<ServiceResponse> ConfirmAccount(string token);
    }
}