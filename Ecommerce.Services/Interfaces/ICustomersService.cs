using Ecommerce.DataAccessLayer.Entities.Product;
using Ecommerce.DataAccessLayer.Models;
using static Ecommerce.Utils.Enums;

namespace Ecommerce.Services.Interfaces
{
    public interface ICustomersService
    {
        Task<IEnumerable<Customer>> GetAll(SearchRequest request);
    }
}