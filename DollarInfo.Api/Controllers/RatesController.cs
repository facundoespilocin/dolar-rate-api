using Microsoft.AspNetCore.Mvc;
using DollarInfo.Services.Interfaces;

namespace DollarInfo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RatesController : ControllerBase
    {
        //private readonly ILogger<DollarRateController> _logger;
        private readonly IRatesService _ratesService;

        public RatesController(IRatesService ratesService)
        {
            //_logger = logger;
            _ratesService = ratesService;
        }

        [HttpGet("exchange-rates")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllExchangeRates()
        {
            var result = await _ratesService.GetAllExchangeRates();

            return Ok(result);
        }

        [HttpGet("market-rates")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllMarketRates()
        {
            var result = await _ratesService.GetAllMarketRates();

            return Ok(result);
        }

        [HttpGet("fixed-term")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFixedTermRates()
        {
            var result = await _ratesService.GetFixedTermRates();

            return Ok(result);
        }
    }
}