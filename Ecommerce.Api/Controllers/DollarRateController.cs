using Microsoft.AspNetCore.Mvc;
using Ecommerce.Services.Interfaces;

namespace Ecommerce.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DollarRateController : ControllerBase
    {
        //private readonly ILogger<DollarRateController> _logger;
        private readonly IDollarRateService _dollarService;

        public DollarRateController(IDollarRateService dolarService)
        {
            //_logger = logger;
            _dollarService = dolarService;
        }

        [HttpGet("exchange-rates")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _dollarService.GetAllExchangeRates();

            return Ok(result);
        }
    }
}