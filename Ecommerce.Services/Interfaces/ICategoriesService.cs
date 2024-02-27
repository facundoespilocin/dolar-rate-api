using Ecommerce.DataAccessLayer.Entities.Category;
using Ecommerce.DataAccessLayer.Models;

namespace Ecommerce.Services.Interfaces
{
    public interface ICategoriesService
    {
        Task<IEnumerable<Category>> GetAll(SearchRequest request);
        Task<ServiceResponse> PostCreateCategory(CreateCategoryDTO request);
    }
}