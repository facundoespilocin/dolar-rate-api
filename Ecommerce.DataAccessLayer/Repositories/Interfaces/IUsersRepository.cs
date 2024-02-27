using Ecommerce.DataAccessLayer.Dtos.User;
using Ecommerce.DataAccessLayer.Entities.Customer;
using Ecommerce.DataAccessLayer.Entities.User;
using Ecommerce.DataAccessLayer.Models;

namespace Ecommerce.DataAccessLayer.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(long userId);
        Task<CustomerDataDTO> GetCustomerByEmail(string email);
        Task<ServiceResponse> Create(CreateUserRequest request);
        Task Update(User user);
        Task Delete(int userId);
        Task<User> GetByCredentials(string email, string password);
        Task<User> GetUserByToken(string token);
        Task UpdateUserPassword(string token, string password);
        Task<User> GetUserByEmailWithPass(string email);
        Task UpdateResetPasswordToken(string email, string token);
        Task ConfirmAccount(string token);
    }
}