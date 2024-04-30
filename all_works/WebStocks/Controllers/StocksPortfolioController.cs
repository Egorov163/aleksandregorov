using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebStocks.DbStuff;
using WebStocks.DbStuff.Models;
using WebStocks.DbStuff.Repositories;
using WebStocks.Models;
using WebStocks.Services;

namespace WebStocks.Controllers
{
    public class StocksPortfolioController : Controller
    {
        private readonly Portfolio _portfolio;

        private StockRepository _stockRepository;
        private DividendRepository _dividendRepository;

        public StocksPortfolioController(Portfolio portfolio, StockRepository stockRepository, DividendRepository dividendRepository)
        {
            _portfolio = portfolio;
            _stockRepository = stockRepository;
            _dividendRepository = dividendRepository;
        }

        public IActionResult Home()
        {
            var dbStocks = _stockRepository.GetStocks(10);

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
            if (!ModelState.IsValid)
            {
                return View(addStockViewModel);
            } 

            var stock = new Stock
            {
                Name = addStockViewModel.Name,
                Price = addStockViewModel.Price
            };
         
            _stockRepository.AddStock(stock);

            return RedirectToAction("Home");
        }

        public IActionResult RemoveStock(int id)
        {
            _stockRepository.Delete(id);           

            return RedirectToAction("Home");
        }

        [HttpPost]
        public IActionResult UpdateStockName(int id, string updName)
        {
            _stockRepository.UpdateStockName(id, updName);
           
            return RedirectToAction("Home");
        }

        //Dividends

        public IActionResult Dividends()
        {
            var dbDividends = _dividendRepository.GetDividendsAndStock(10);

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

            viewModel.Stocks = _stockRepository.GetAll()
                .Where(x=>x.IsDeleted==false)
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddDividend(int price, int stockId)
        {
            var stock = _stockRepository.GetStock(stockId);

            var dividend = new Dividend
            {
                Price = price,
                Stock = stock
            };

            _dividendRepository.AddDividend(dividend);

            return RedirectToAction("Dividends");
        }

        public IActionResult RemoveDividend(int id)
        {
            _dividendRepository.Delete(id);

            return RedirectToAction("Dividends");
        }
    }
}
