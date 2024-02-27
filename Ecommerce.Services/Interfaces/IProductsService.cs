using Ecommerce.DataAccessLayer.Entities.Product;
using Ecommerce.DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using static Ecommerce.Utils.Enums;

namespace Ecommerce.Services.Interfaces
{
    public interface IProductsService
    {
        Task<IEnumerable<ProductDTO>> GetAll(SearchRequest request);
        Task<ProductDTO> GetById(long productId);
        Task<ServiceResponse> PostCreateProduct(ProductDTO request);
        Task<ServiceResponse> PostLoadProducts(LoadProductsDTO request);
        Task<IEnumerable<Product>> PostReadProducts(IFormFile file, DataSourceTypes dataSource);
    }
}