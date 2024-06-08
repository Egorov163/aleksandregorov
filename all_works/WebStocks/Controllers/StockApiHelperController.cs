using Microsoft.AspNetCore.Mvc;

namespace WebStocks.Controllers
{
    public class StockApiHelperController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
