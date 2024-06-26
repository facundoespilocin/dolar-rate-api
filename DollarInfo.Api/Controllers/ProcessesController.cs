﻿using Microsoft.AspNetCore.Mvc;
using DollarInfo.Services.Interfaces;
using static DollarInfo.Utils.Enums;

namespace DollarInfo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProcessesController : ControllerBase
    {
        //private readonly ILogger<DollarRateController> _logger;
        private readonly IProcessesService _processService;

        public ProcessesController(IProcessesService processService)
        {
            //_logger = logger;
            _processService = processService;
        }

        [HttpPost("inflation-index")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> PostInflationIndex(
            InflationIndexTypes inflationIndexType = InflationIndexTypes.Monthly,
            InsertionTypes insertionType = InsertionTypes.Single)
        {
            await _processService.PostInflationIndex(inflationIndexType, insertionType);

            return Ok();
        }
    }
}