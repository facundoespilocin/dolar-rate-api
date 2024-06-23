using Microsoft.AspNetCore.Mvc;
using DollarInfo.Services.Interfaces;
using static DollarInfo.Utils.Enums;

namespace DollarInfo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IndexesController : ControllerBase
    {
        //private readonly ILogger<DollarRateController> _logger;
        private readonly IIndexesService _indexesService;

        public IndexesController(IIndexesService indexesService)
        {
            //_logger = logger;
            _indexesService = indexesService;
        }

        [HttpGet("inflation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMonthlyInflationIndexes(InflationIndexTypes inflationIndexType = InflationIndexTypes.Monthly)
        {
            var result = await _indexesService.GetInflationIndexes(inflationIndexType);

            return Ok(result);
        }
    }
}