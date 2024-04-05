using WebStocks.Models;

namespace WebStocks.Services
{
    public class Portfolio
    {
        public int GetPortfolioValue(List<StockViewModel> stocks)
        { 
           var value =  stocks.Sum(x=>x.Price);
            return value;
        }
    }
}
