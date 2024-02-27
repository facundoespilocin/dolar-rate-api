using Microsoft.AspNetCore.Mvc;
using Ecommerce.Services.Interfaces;
using static Ecommerce.Utils.Enums;
using Ecommerce.DataAccessLayer.Models;
using Ecommerce.DataAccessLayer.Entities.Product;

namespace Ecommerce.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductsService _productsService;

        public class ReadProducts
        {
            public IFormFile File { get; set; }
            public DataSourceTypes DataSource { get; set; }
        }

        public ProductsController(ILogger<ProductsController> logger, IProductsService productsService)
        {
            _logger = logger;
            _productsService = productsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] SearchRequest request)
        {
            var result = await _productsService.GetAll(request);

            return Ok(result);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetById(long productId)
        {
            var result = await _productsService.GetById(productId);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductDTO request)
        {
            var result = await _productsService.PostCreateProduct(request);

            return Ok(result);
        }

        [HttpPost("read-products")]
        public async Task<IActionResult> PostReadProducts([FromForm] ReadProducts request)
        {
            var result = await _productsService.PostReadProducts(request.File, request.DataSource);

            return Ok(result);
        }

        [HttpPost("load-products")]
        public async Task<IActionResult> PostLoadProducts([FromBody] LoadProductsDTO request)
        {
            var result = await _productsService.PostLoadProducts(request);

            return Ok(result);
        }
    }
}