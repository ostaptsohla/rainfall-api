using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Rainfall_API.Models;
using Rainfall_API.Services;


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

        [HttpGet("/rainfall/id/{stationId}/readings")]
        public async Task<IActionResult> GetRainfallReadingsAsync([FromRoute] string stationId, [FromQuery, Range(1, 100)] int count = 10)
        {
            var rainfallReadingResponse = await _rainfallReadingsService.GetRainfallReadingsAsync(stationId, count);

            return Ok(rainfallReadingResponse);
        }
    }
}
