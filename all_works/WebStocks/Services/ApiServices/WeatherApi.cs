using WebStocks.Services.ApiServices.Dto;

namespace WebStocks.Services.ApiServices
{
    public class WeatherApi
    {
        private HttpClient _httpClient;

        public WeatherApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<WeatherDto?> GetWeatherUrlByCoordinate(string latitude, string longitude)
        {
            return _httpClient.GetFromJsonAsync<WeatherDto>($"/v1/forecast?latitude={latitude}&longitude={longitude}&hourly=temperature_2m");
        }
    }
}
