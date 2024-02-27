using Microsoft.AspNetCore.Mvc;
using Ecommerce.Services.Interfaces;
using static Ecommerce.Utils.Enums;
using Ecommerce.DataAccessLayer.Models;
using Ecommerce.Services;
using Ecommerce.DataAccessLayer.Entities.Product;
using Ecommerce.DataAccessLayer.Entities.Category;

namespace Ecommerce.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly ICategoriesService _categoriesService;

        public CategoriesController(ILogger<ProductsController> logger, ICategoriesService categoriesService)
        {
            _logger = logger;
            _categoriesService = categoriesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] SearchRequest request)
        {
            var result = await _categoriesService.GetAll(request);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCategoryDTO request)
        {
            var result = await _categoriesService.PostCreateCategory(request);

            return Ok(result);
        }
    }
}