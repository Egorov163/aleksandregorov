
using Microsoft.EntityFrameworkCore;
using WebStocks.DbStuff.Models;

namespace WebStocks.DbStuff.Repositories
{
    public class StockRepository : BaseRepository<Stock>
    {
        public StockRepository(WebDbContext _dbContext) : base(_dbContext) { }

        public void UpdateStockName(int id, string updName)
        {
            _entities.First(x => x.Id == id).Name = updName;
            _dbContext.SaveChanges();
        }

        public override IEnumerable<Stock> Get(int maxCount = 10)
        {
            return _entities
                .Where(x => x.IsDeleted == false)
                .Include(x => x.Owner)
                .Take(maxCount)
                .ToList();
        }

        public Stock GetByIdWithOwner(int StockId)
        {
            return _entities
                .Include(x => x.Owner)
                .First(x => x.Id == StockId);
        }

        public override void Delete(int id)
        {
            var dbModel = _entities
                .First(x => x.Id == id)
                .IsDeleted = true;
            _dbContext.SaveChanges();
        }

        public void UpdateLogo(int stockId, string logoUrl)
        {
            GetById(stockId).LogoUrl = logoUrl;
            _dbContext.SaveChanges();
        }
    }
}
