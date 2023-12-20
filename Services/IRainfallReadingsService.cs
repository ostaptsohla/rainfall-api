using Rainfall_API.Models;

namespace Rainfall_API.Services
{
    public interface IRainfallReadingsService
    {
        Task<RainfallReadingResponse> GetRainfallReadingsAsync(string stationId, int count);
    }
}
