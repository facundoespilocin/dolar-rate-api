using Microsoft.AspNetCore.Mvc;
using DollarInfo.Services.Interfaces;
using DollarInfo.DAL.Models;

namespace DollarInfo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BugReportController : ControllerBase
    {
        //private readonly ILogger<DollarRateController> _logger;
        private readonly IBugReportService _bugReportService;

        public BugReportController(IBugReportService bugReportService)
        {
            //_logger = logger;
            _bugReportService = bugReportService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromQuery] BugReportRequest request)
        {
            try
            {
                await _bugReportService.Post(request);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}