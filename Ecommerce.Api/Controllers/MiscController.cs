using Microsoft.AspNetCore.Mvc;
using Ecommerce.Services.Interfaces;

namespace Ecommerce.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MiscController : ControllerBase
    {
        private readonly ILogger<MiscController> _logger;
        private readonly IMiscService _miscService;

        public MiscController(ILogger<MiscController> logger, IMiscService miscService)
        {
            _logger = logger;
            _miscService = miscService;
        }

        [HttpGet("countries")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCountries()
        {
            var result = await _miscService.GetCountries();

            return Ok(result);
        }

        [HttpGet("countries/{countryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCountryById(int countryId)
        {
            var result = await _miscService.GetCountryById(countryId);
                
            return Ok(result);
        }

        [HttpGet("industries")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetIndustries()
        {
            var result = await _miscService.GetIndustries();

            return Ok(result);
        }
    }
}