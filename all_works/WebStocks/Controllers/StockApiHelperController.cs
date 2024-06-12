using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using WebStocks.Controllers.ApiControllers;
using WebStocks.Models.StockApiHelper;
using WebStocks.Services;

namespace WebStocks.Controllers
{
    public class StockApiHelperController : Controller
    {
        private ReflectionService _reflectionService;
        public StockApiHelperController(ReflectionService reflectionService)
        {
            _reflectionService = reflectionService;
        }
        public IActionResult Index()
        {
            var stockApiControllerType = typeof(StockApiControllers);

            var ControllerTypes = Assembly.GetAssembly(stockApiControllerType)
                .GetTypes()
                .Where(x => x.GetCustomAttributes<ApiControllerAttribute>().Any());

            var apiHelperViewModel = _reflectionService.BuilderApiHelperViewModel(stockApiControllerType);

            return View(apiHelperViewModel);
               
        }
        
    }
}
