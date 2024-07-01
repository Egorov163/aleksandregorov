using WebStocks.Services.ApiServices.Dto;

namespace WebStocks.Services.ApiServices
{
    public class ExchangeRateApi
    {
        private HttpClient _httpClient;

        public ExchangeRateApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<ExchangeRateDto?> GetExchangeRateUrl()
        {
            return _httpClient.GetFromJsonAsync<ExchangeRateDto>($"/daily_json.js");
        }
    }
}
