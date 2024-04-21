using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebStocks.DbStuff;
using WebStocks.DbStuff.Models;
using WebStocks.Models;
using WebStocks.Services;

namespace WebStocks.Controllers
{
    public class StocksPortfolioController : Controller
    {
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

        //Dividends

        public IActionResult Dividends()
        {
            var dbDividends = _webDbContext.Dividends.Include(x => x.Stock).Take(10).ToList();

            var viewModel = dbDividends.Select(dbDividends => new DividendViewModel
            {
                Id = dbDividends.Id,
                NameStock = dbDividends.Stock.Name,
                Price = dbDividends.Price
            })
                .ToList();

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult AddDividend()
        {
            var viewModel = new AddDividendViewModel();

            viewModel.Stocks = _webDbContext.Stocks
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddDividend(int price, int stockId)
        {
            var stock = _webDbContext.Stocks.First(x => x.Id == stockId);

            var dividend = new Dividend
            {
                Price = price,
                Stock = stock
            };

            _webDbContext.Add(dividend);

            _webDbContext.SaveChanges();

            return RedirectToAction("Dividends");
        }

        public IActionResult RemoveDividend(int id)
        {
            var dividend = _webDbContext.Dividends.First(x => x.Id == id);
            _webDbContext.Remove(dividend);
            _webDbContext.SaveChanges();

            return RedirectToAction("Dividends");
        }
    }
}
