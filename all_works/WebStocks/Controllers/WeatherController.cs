using Microsoft.AspNetCore.Mvc;
using WebStocks.Models.ExchangeRate;
using WebStocks.Models.Weather;
using WebStocks.Services.ApiServices;

namespace WebStocks.Controllers
{
    public class WeatherController : Controller
    {
        private WeatherApi _weatherApi;

        public WeatherController(WeatherApi weatherApi)
        {
            _weatherApi = weatherApi;
        }

        public IActionResult Index()
        {
            var viewModel = new WeatherIndexViewModel();

            var weatherTask = _weatherApi.GetWeatherUrlByCoordinate("53.94", "51.15");

            Task.WaitAll(weatherTask);

            viewModel.TemperaturesFor24Hours = weatherTask.Result.hourly.temperature_2m;

            return View(viewModel);
        }
    }
}
