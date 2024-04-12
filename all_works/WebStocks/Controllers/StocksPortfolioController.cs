using Microsoft.AspNetCore.Mvc;
using WebStocks.DbStuff;
using WebStocks.DbStuff.Models;
using WebStocks.Models;
using WebStocks.Services;

namespace WebStocks.Controllers
{
    public class StocksPortfolioController : Controller
    {
        public static List<StockViewModel> stocksViewModels = new List<StockViewModel>();

        private readonly Portfolio _portfolio;
        private WebDbContext _webDbContext { get; set; }

        public StocksPortfolioController(Portfolio portfolio, WebDbContext webDbContext)
        {
            _portfolio = portfolio;
            _webDbContext = webDbContext;
        }

        public IActionResult Home()
        {
            var dbStocks = _webDbContext.Stocks.Take(10).ToList();

            var viewModel = dbStocks.Select(dbStock => new StockViewModel
            {
                Id = dbStock.Id,
                Name = dbStock.Name,
                Price = dbStock.Price
            })
                .ToList();

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult AddStock()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddStock(AddStockViewModel addStockViewModel)
        {
            var stock = new Stock
            {
                Name = addStockViewModel.Name,
                Price = addStockViewModel.Price
            };

            _webDbContext.Stocks.Add(stock);
            _webDbContext.SaveChanges();

            return RedirectToAction("Home");
        }

        public IActionResult RemoveStock(int id)
        {
            var stock = _webDbContext.Stocks.First(x => x.Id == id);
            _webDbContext.Remove(stock);
            _webDbContext.SaveChanges();

            return RedirectToAction("Home");
        }

        [HttpPost]
        public IActionResult UpdateStockName(int id, string updName)
        {
            _webDbContext.Stocks.First(x => x.Id == id).Name = updName;
            _webDbContext.SaveChanges();

            return RedirectToAction("Home");
        }


    }
}
