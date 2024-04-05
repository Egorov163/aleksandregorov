using Microsoft.AspNetCore.Mvc;
using WebStocks.Models;
using WebStocks.Services;

namespace WebStocks.Controllers
{
    public class StocksPortfolioController : Controller
    {
        public static List<StockViewModel> stockViewModels = new List<StockViewModel>();

        private readonly Portfolio _portfolio;

        public StocksPortfolioController(Portfolio portfolio)
        {
            _portfolio = portfolio;
        }

        public IActionResult Home()
        {          
            return View(stockViewModels);
        }

        [HttpGet]
        public IActionResult AddStock()
        {          
            return View();
        }

        [HttpPost]
        public IActionResult AddStock(AddStockViewModel addStockViewModel)
        {
            stockViewModels.Add(new StockViewModel 
            {
                Name = addStockViewModel.Name,
                Price = addStockViewModel.Price
            });
            return RedirectToAction("Home");
        }


        public IActionResult RemoveStock(string name)
        {
            var stock = stockViewModels.First(x => x.Name == name);
            stockViewModels.Remove(stock);

            return RedirectToAction("Home");
        }


    }
}
