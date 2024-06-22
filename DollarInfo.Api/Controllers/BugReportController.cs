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

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Post(BugReportRequest request)
        {
            await _bugReportService.Post(request);

            return Ok();
        }
    }
}