using Microsoft.AspNetCore.Mvc;
using WebStocks.Models;

namespace WebStocks.Controllers
{
    public class StocksPortfolioController : Controller
    {
        public static List<StockViewModel> stockViewModels = new List<StockViewModel>();

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


    }
}
