using Microsoft.AspNetCore.Mvc;
using Rainfall_API.Models;
using System.ComponentModel.DataAnnotations;

namespace Rainfall_API.Services
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class RainfallReadingsController : ControllerBase
    {
        private readonly IRainfallReadingsService _rainfallReadingsService;

        public RainfallReadingsController(IRainfallReadingsService rainfallReadingsService)
        {
            _rainfallReadingsService = rainfallReadingsService;
        }

        [ProducesResponseType(typeof(RainfallReadingResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet("/rainfall/id/{stationId}/readings")]
        public async Task<IActionResult> GetRainfallReadingsAsync([FromRoute] string stationId, [FromQuery, Range(1, 100)] int count = 10)
        {
            try
            {
                var rainfallReadingResponse = await _rainfallReadingsService.GetRainfallReadingsAsync(stationId, count);
                if (rainfallReadingResponse.Readings.Count == 0)
                {
                    return BadRequest(new ErrorResponse() { Message = "No readings found for the specified stationId" });
                }

                return Ok(rainfallReadingResponse);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse { Message = "Internal server error" });
            }
        }
    }
}
