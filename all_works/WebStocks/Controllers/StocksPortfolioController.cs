using Microsoft.AspNetCore.Mvc;
using WebStocks.Models;
using WebStocks.Services;

namespace WebStocks.Controllers
{
    public class StocksPortfolioController : Controller
    {
        public static List<StockViewModel> stocksViewModels = new List<StockViewModel>();

        private readonly Portfolio _portfolio;

        public StocksPortfolioController(Portfolio portfolio)
        {
            _portfolio = portfolio;
        }

        public IActionResult Home()
        {

            return View(stocksViewModels);
        }

        [HttpGet]
        public IActionResult AddStock()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddStock(AddStockViewModel addStockViewModel)
        {
            stocksViewModels.Add(new StockViewModel
            {
                Name = addStockViewModel.Name,
                Price = addStockViewModel.Price
            });
            return RedirectToAction("Home");
        }

        public IActionResult RemoveStock(string name)
        {
            var stock = stocksViewModels.First(x => x.Name == name);
            stocksViewModels.Remove(stock);

            return RedirectToAction("Home");
        }

        [HttpPost]
        public IActionResult UpdateStockName(string name, string updName)
        {
            var stock = stocksViewModels.First(x => x.Name == name).Name = updName;

            return RedirectToAction("Home");
        }


    }
}
