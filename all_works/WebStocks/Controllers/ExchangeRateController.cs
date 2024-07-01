using Microsoft.AspNetCore.Mvc;
using WebStocks.Models.ExchangeRate;
using WebStocks.Services.ApiServices;

namespace WebStocks.Controllers
{
    public class ExchangeRateController : Controller
    {
        private ExchangeRateApi _exchangeRateApi;

        public ExchangeRateController(ExchangeRateApi exchangeRateApi)
        {
            _exchangeRateApi = exchangeRateApi;
        }
        public IActionResult Index()
        {
            var viewModel = new ExchangeRateIndexViewModel();
            
            var exchangeRateTask = _exchangeRateApi.GetExchangeRateUrl();

            Task.WaitAll(exchangeRateTask);

            viewModel.Name = exchangeRateTask.Result.Valute.USD.Name;
            viewModel.Price = exchangeRateTask.Result.Valute.USD.Value;

            return View(viewModel);
        }
    }
}
