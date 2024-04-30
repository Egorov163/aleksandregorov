using Microsoft.EntityFrameworkCore;
using WebStocks.DbStuff.Models;

namespace WebStocks.DbStuff.Repositories
{
    public class DividendRepository
    {
        private WebDbContext _db;

        public DividendRepository(WebDbContext db)
        {
            _db = db;
        }
        public IEnumerable<Dividend> GetDividendsAndStock(int maxCount)
        {
            return _db.Dividends
                .Include(x => x.Stock)
                .Take(maxCount)
                .ToList();
        }

        public int AddDividend(Dividend dividend)
        {
            _db.Dividends.Add(dividend);
            _db.SaveChanges();
            return dividend.Id;
        }

        public void Delete(int id)
        {
            var dividend = _db.Dividends.First(x => x.Id == id);
            _db.Remove(dividend);
            _db.SaveChanges();
        }
    }
}
