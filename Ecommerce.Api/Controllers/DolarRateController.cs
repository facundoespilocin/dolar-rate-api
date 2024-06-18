using Microsoft.AspNetCore.Mvc;
using Ecommerce.Services.Interfaces;

namespace Ecommerce.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DolarRateController : ControllerBase
    {
        private readonly ILogger<DolarRateController> _logger;
        private readonly IDolarRateService _dolarService;

        public DolarRateController(ILogger<DolarRateController> logger, IDolarRateService dolarService)
        {
            _logger = logger;
            _dolarService = dolarService;
        }

        [HttpGet("exchange-rates")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _dolarService.GetAllExchangeRates();

            return Ok(result);
        }
    }
}