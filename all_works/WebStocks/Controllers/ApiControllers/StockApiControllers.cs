using Microsoft.AspNetCore.Mvc;
using WebStocks.Controllers.CustomAuthAttributes;
using WebStocks.DbStuff.Repositories;

namespace WebStocks.Controllers.ApiControllers
{
    [ApiController]
    [Route("/StocksPortfolio/{action}")]
    public class StockApiControllers : Controller
    {
        private StockRepository _stockRepository;

        public StockApiControllers(StockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }
        [AdminOnlyAttributes]
        public int PricePlusOne(int stockId)
        {
            return _stockRepository.AddOneCoin(stockId);
        }
    }
}
