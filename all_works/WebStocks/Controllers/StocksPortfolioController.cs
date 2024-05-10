using Microsoft.AspNetCore.Authorization;
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
        private AuthService _authService;
        private IWebHostEnvironment _webHostEnvironment;
        private StockRepository _stockRepository;
        private DividendRepository _dividendRepository;

        public StocksPortfolioController(Portfolio portfolio,
            StockRepository stockRepository,
            DividendRepository dividendRepository,
            IWebHostEnvironment webHostEnvironment,
            AuthService authService)
        {
            _portfolio = portfolio;
            _stockRepository = stockRepository;
            _dividendRepository = dividendRepository;
            _webHostEnvironment = webHostEnvironment;
            _authService = authService;
        }

        public IActionResult Home()
        {
            var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "name")?.Value ?? "Гость";

            var dbStocks = _stockRepository.Get(10);

            var viewModel = dbStocks.Select(dbStock => new StockViewModel
            {
                Id = dbStock.Id,
                Name = dbStock.Name,
                Price = dbStock.Price,
                OwnerName = dbStock.Owner?.Login ?? "Неизвестный"
            })
                .ToList();

            return View(viewModel);
        }
        public IActionResult StockInformation(int stockId)
        {
            var dbModel = _stockRepository.GetById(stockId);

            var viewModel = new StockInformationViewModel
            {
                Id = dbModel.Id,
                Name = dbModel.Name,
                Price = dbModel.Price,
                LogoUrl = dbModel.LogoUrl,
                DateBuy = dbModel.DateBuy
            };
            return View(viewModel);
        }

        public IActionResult UpdateLogo(int stockId, IFormFile logo)
        {
            var extension = Path.GetExtension(logo.FileName);
            var fileName = $"stockLogo{stockId}{extension}";
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "logo", fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                logo.CopyTo(fileStream);
            }

            var logoUrl = $"/images/logo/{fileName}";
            _stockRepository.UpdateLogo(stockId, logoUrl);

            return RedirectToAction("StockInformation", new { stockId });
        }

        [HttpGet]
        [Authorize]
        public IActionResult AddStock()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddStock(AddStockViewModel addStockViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(addStockViewModel);
            }

            var stock = new Stock
            {
                Name = addStockViewModel.Name,
                Price = addStockViewModel.Price,
                DateBuy = addStockViewModel.DateBuy,
                Owner = _authService.GetCurrentUser()
            };

            _stockRepository.Add(stock);

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
            var dbDividends = _dividendRepository.Get(10);

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
        [Authorize]
        public IActionResult AddDividend()
        {
            var viewModel = new AddDividendViewModel();

            viewModel.Stocks = _stockRepository.GetAll()
                .Where(x => x.IsDeleted == false)
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();

            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddDividend(int price, int stockId)
        {
            var stock = _stockRepository.GetById(stockId);

            var dividend = new Dividend
            {
                Price = price,
                Stock = (Stock)stock
            };

            _dividendRepository.Add(dividend);

            return RedirectToAction("Dividends");
        }

        public IActionResult RemoveDividend(int id)
        {
            _dividendRepository.Delete(id);

            return RedirectToAction("Dividends");
        }
    }
}
