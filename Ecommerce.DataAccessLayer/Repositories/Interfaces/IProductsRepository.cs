using Ecommerce.DataAccessLayer.Entities.Product;
using Ecommerce.DataAccessLayer.Entities.User;
using Ecommerce.DataAccessLayer.Models;

namespace Ecommerce.DataAccessLayer.Repositories.Interfaces
{
    public interface IProductsRepository
    {
        Task<IEnumerable<ProductDTO>> GetAll(SearchRequest request);
        Task<ProductDetailDTO> GetById(long productId);
        Task<IEnumerable<ProductDetailDTO>> GetByIds(long productId);
        Task<IEnumerable<ProductDTO>> GetAllFiltered(SearchRequest request);
        Task<ServiceResponse> InsertProducts(IEnumerable<Product> products);
        Task<ServiceResponse> InsertProduct(ProductDTO request);
    }
}