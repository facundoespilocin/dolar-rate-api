using AutoMapper;
using Ecommerce.DataAccessLayer.Entities.Category;
using Ecommerce.DataAccessLayer.Entities.Product;
using Ecommerce.DataAccessLayer.Models;
using Ecommerce.DataAccessLayer.Repositories.Interfaces;
using Ecommerce.Services.Interfaces;
using Ecommerce.Utils.Settings;
using Microsoft.Extensions.Options;

namespace Ecommerce.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;

        public CategoriesService(ICategoriesRepository categoriesRepository, IOptions<AppSettings> appSettings, IMapper mapper)
        {
            _categoriesRepository = categoriesRepository;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Category>> GetAll(SearchRequest request)
        {
            var categories = await _categoriesRepository.GetAll(request);

            return categories;
        }

        public async Task<ServiceResponse> PostCreateCategory(CreateCategoryDTO request)
        {
            ServiceResponse response = new();

            try
            {
                response = await _categoriesRepository.InsertCategory(request);
            }
            catch (Exception e)
            {
                response.Metadata.Status = System.Net.HttpStatusCode.BadRequest;
                response.Metadata.Message = e.Message;
            }

            return response;
        }
    }
}