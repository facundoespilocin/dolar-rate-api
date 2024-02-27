using Ecommerce.DataAccessLayer.Entities.Product;
using Ecommerce.DataAccessLayer.Models;

namespace Ecommerce.DataAccessLayer.Repositories.Interfaces
{
    public interface ICustomersRepository
    {
        Task<IEnumerable<Customer>> GetAll(SearchRequest request);
        Task<Customer> GetById(long categoryId);
        Task<ServiceResponse> InsertProducts(IEnumerable<Customer> categories);
    }
}