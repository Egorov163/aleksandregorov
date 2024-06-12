using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebStocks.BusinessService;
using WebStocks.Controllers.CustomAuthAttributes;
using WebStocks.DbStuff.Models;
using WebStocks.DbStuff.Repositories;
using WebStocks.Models;
using WebStocks.Services;

namespace WebStocks.Controllers
{
    public class StocksPortfolioController : Controller
    {
        private readonly PortfolioHelper _portfolio;
        private AuthService _authService;
        private IWebHostEnvironment _webHostEnvironment;
        private StockRepository _stockRepository;
        private DividendRepository _dividendRepository;
        private StockPermissions _stockPermissions;
        private DividendPermissions _dividendPermissions;
        private StockBusinnesService _stockBusinnesService;
        private DividendBusinnesService _dividendBusinnesService;

        public StocksPortfolioController(PortfolioHelper portfolio,
            StockRepository stockRepository,
            DividendRepository dividendRepository,
            IWebHostEnvironment webHostEnvironment,
            AuthService authService,
            StockPermissions stockPermissions,
            DividendPermissions dividendPermissions,
            StockBusinnesService stockBusinnesService,
            DividendBusinnesService dividendBusinnesService)
        {
            _portfolio = portfolio;
            _stockRepository = stockRepository;
            _dividendRepository = dividendRepository;
            _webHostEnvironment = webHostEnvironment;
            _authService = authService;
            _stockPermissions = stockPermissions;
            _dividendPermissions = dividendPermissions;
            _stockBusinnesService = stockBusinnesService;
            _dividendBusinnesService = dividendBusinnesService;
        }

        public IActionResult Home()
        {          
            var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "name")?.Value ?? "Гость";

            var StockVewModel = _stockBusinnesService.GetTop10StocksForMainPage();

            var viewModel = new StockIndexViewModel
            {
                Stocks = StockVewModel,
                UserName = userName,
                IsAdmin = _authService.IsAdmin()
            };

            return View(viewModel);
        }
        public IActionResult DeletedStocks()
        {
            var StockViewModel = _stockBusinnesService.GetDeletedStocksForMainPage();

            var viewModel = new StockDeletedViewModel
            {
                Stocks = StockViewModel,
                IsAdmin = _authService.IsAdmin()
            };

            return View(viewModel);
        }
        public IActionResult StockInformation(int stockId)
        {
            var viewModel = _stockBusinnesService.GetStockInformationForMainPage(stockId);    
                
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
                Owner = _authService.GetCurrentUser(),
                IsDeleted = false
            };

            _stockRepository.Add(stock);

            return RedirectToAction("Home");
        }

        [AdminOnlyAttributes]
        public IActionResult AddRandomStock(AddStockViewModel addStockViewModel)
        {
            var stock = new Stock
            {
                Name = "ОФЗ",
                Price = 1000,
                DateBuy = DateTime.Now,
                Owner = _authService.GetCurrentUser(),
                IsDeleted = false
            };

            _stockRepository.Add(stock);

            return RedirectToAction("Home");
        }

        public IActionResult RemoveStock(int id)
        {
            var dbStock = _stockRepository.GetByIdWithOwner(id);

            if (!_stockPermissions.CanDetele(dbStock))
            {
                throw new Exception("Всё, приехали");
            }

            _stockRepository.Delete(id);

            return RedirectToAction("Home");
        }

        public IActionResult RemoveStockForStockDeleted(int id)
        {
            var dbStock = _stockRepository.GetByIdWithOwner(id);

            if (!_stockPermissions.CanDetele(dbStock))
            {
                throw new Exception("Всё, приехали");
            }

            _stockRepository.DeleteFromDb(id);

            return RedirectToAction("DeletedStocks");
        }

        [HttpPost]
        public IActionResult UpdateStockName(int id, string updName)
        {
            _stockRepository.UpdateStockName(id, updName);

            return RedirectToAction("Home");
        }

        //Dividends

        [Authorize]
        public IActionResult DividendIndex()
        {
            var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "name")?.Value ?? "Гость";

            var DividendViewModel = _dividendBusinnesService.GetTop10DividendsForMainPage();


            var viewModel = new DividendIndexViewModel
            {
                Dividends = DividendViewModel,
                UserName = userName,
                IsAdmin = _authService.IsAdmin()
            };

            return View(viewModel);
        }

        [HttpGet]
        [Authorize]
        public IActionResult AddDividend()
        {
            var viewModel = new AddDividendViewModel();

            viewModel.Stocks = _stockRepository.GetUserStocks(_authService.GetCurrentUserId())
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
                Stock = (Stock)stock,
                Owner = _authService.GetCurrentUser()
            };

            _dividendRepository.Add(dividend);

            return RedirectToAction("DividendIndex");
        }

        public IActionResult RemoveDividend(int id)
        {
            _dividendRepository.Delete(id);

            return RedirectToAction("DividendIndex");
        }
    }
}
