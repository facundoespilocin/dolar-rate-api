using Microsoft.AspNetCore.Mvc;
using Ecommerce.Services.Interfaces;
using Ecommerce.DataAccessLayer.Models;

namespace Ecommerce.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly ICustomersService _customersService;

        public CustomersController(ILogger<CustomersController> logger, ICustomersService customersService)
        {
            _logger = logger;
            _customersService = customersService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] SearchRequest request)
        {
            var result = await _customersService.GetAll(request);

            return Ok(result);
        }
    }
}