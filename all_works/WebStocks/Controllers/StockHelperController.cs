using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using WebStocks.Controllers.ApiControllers;
using WebStocks.Models.StockApiHelper;
using WebStocks.Models.StockHelper;
using WebStocks.Services;

namespace WebStocks.Controllers
{
    public class StockHelperController : Controller
    {
        private ReflectionService _reflectionService;
        public StockHelperController(ReflectionService reflectionService)
        {
            _reflectionService = reflectionService;
        }
        public IActionResult Index()
        {
            var indexViewMidel = new IndexHelperViewModel();

            var stockApiControllerType = typeof(StockApiControllers);

            var ApiControllerTypes = Assembly.GetAssembly(stockApiControllerType)
                .GetTypes()
                .Where(x => x.GetCustomAttributes<ApiControllerAttribute>().Any());

            var apiHelperViewModel = _reflectionService.BuilderHelperViewModel(stockApiControllerType);


            var stockControllerType = typeof(StocksPortfolioController);

            var controllerTypes = Assembly.GetAssembly(stockControllerType)
                .GetTypes()
                .Where(x => x.GetCustomAttributes<ApiControllerAttribute>().Any());

            var helperViewModel = _reflectionService.BuilderHelperViewModel(stockControllerType);

            indexViewMidel.HelperApiViewModel = apiHelperViewModel;
            indexViewMidel.HelperViewModel = helperViewModel;

            return View(indexViewMidel);
               
        }
        
    }
}
