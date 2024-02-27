using Ecommerce.DataAccessLayer.Entities.Category;
using Ecommerce.DataAccessLayer.Models;

namespace Ecommerce.DataAccessLayer.Repositories.Interfaces
{
    public interface ICategoriesRepository
    {
        Task<IEnumerable<Category>> GetAll(SearchRequest request);
        Task<Category> GetById(long categoryId);
        Task<ServiceResponse> InsertCategories(IEnumerable<Category> categories);
        Task<ServiceResponse> InsertCategory(CreateCategoryDTO request);
    }
}