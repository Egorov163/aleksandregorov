using Microsoft.EntityFrameworkCore;
using WebStocks.DbStuff.Models;

namespace WebStocks.DbStuff.Repositories
{
    public class DividendRepository : BaseRepository<Dividend>
    {
        public DividendRepository(WebDbContext _dbContext) : base(_dbContext) { }

        public override IEnumerable<Dividend> Get(int maxCount)
        {
            return _entities
               .Include(x => x.Stock)
               .Include(x => x.Owner)
               .Take(maxCount)
               .ToList();
        }
    }
}
