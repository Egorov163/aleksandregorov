using Microsoft.EntityFrameworkCore;
using WebStocks.DbStuff.Models;

namespace WebStocks.DbStuff.Repositories
{
    public abstract class BaseRepository<DbModel>
        where DbModel : BaseModel
    {
        protected readonly WebDbContext _dbContext;
        protected readonly DbSet<DbModel> _entities;

        protected BaseRepository(WebDbContext dbContext)
        {
            _dbContext = dbContext;
            _entities = _dbContext.Set<DbModel>();
        }

        public virtual DbModel GetById(int id)
        {
            return _entities.First(x => x.Id == id);
        }

        public virtual IEnumerable<DbModel> Get(int maxCount)
        {
            return _entities
                .Take(maxCount)
                .ToList();
        }

        public virtual IEnumerable<DbModel> GetAll()
        {
            return _entities
                .ToList();
        }

        public virtual void Delete(int id)
        {
            var dbModel = _entities.First(x => x.Id == id);
            _entities.Remove(dbModel);
            _dbContext.SaveChanges();
        }

        public virtual int Add(DbModel dbModel)
        {
            _entities.Add(dbModel);
            _dbContext.SaveChanges();
            return dbModel.Id;
        }

        public virtual bool Any()
        {
            return _entities.Any();
        }
    }
}
