using Rainfall_API.Models;
using System.Text.Json;

namespace Rainfall_API.Services
{
    public class RainfallReadingsService : IRainfallReadingsService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public RainfallReadingsService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<RainfallReadingResponse> GetRainfallReadingsAsync(string stationId, int count)
        {
            var url = $"https://environment.data.gov.uk/flood-monitoring/id/stations/{stationId}/readings?_sorted&_limit={count}";
            var httpClient = _httpClientFactory.CreateClient();

            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var jsonDocument = await JsonDocument.ParseAsync(response.Content.ReadAsStream());
            JsonElement itemsArray = jsonDocument.RootElement.GetProperty("items");
            List<RainfallReading>? rainfallReadings = JsonSerializer.Deserialize<List<RainfallReading>>(itemsArray.GetRawText());

            return new RainfallReadingResponse { Readings = rainfallReadings };
        }
    }
}
