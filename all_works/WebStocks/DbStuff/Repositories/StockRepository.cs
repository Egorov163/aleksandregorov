
using WebStocks.DbStuff.Models;

namespace WebStocks.DbStuff.Repositories
{
    public class StockRepository
    {
        private WebDbContext _db;

        public StockRepository(WebDbContext db)
        {
            _db = db;
        }

        public Stock GetStock(int stockId)
        {
            return _db.Stocks.First(x => x.Id == stockId);
        }

        public IEnumerable<Stock> GetStocks(int maxCount)
        {
            return _db.Stocks
                .Where(x => x.IsDeleted == false)
                .Take(maxCount)
                .ToList();
        }

        public IEnumerable<Stock> GetAll()
        {
            return _db.Stocks
                .ToList();
        }

        public void Delete(int id)
        {
            _db.Stocks.First(x => x.Id == id).IsDeleted = true;
            _db.SaveChanges();
        }

        public void UpdateStockName(int id, string updName)
        {
            _db.Stocks.First(x => x.Id == id).Name = updName;
            _db.SaveChanges();
        }

        public int AddStock(Stock stock)
        {
            _db.Stocks.Add(stock);
            _db.SaveChanges();
            return stock.Id;
        }


    }
}
