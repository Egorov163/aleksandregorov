using WebStocks.Models;

namespace WebStocks.Services
{
    public class PortfolioHelper
    {
        public int GetPortfolioValue(List<StockViewModel> stocks)
        {
            var value = stocks.Sum(x => x.Price);
            return value;
        }
    }
}
