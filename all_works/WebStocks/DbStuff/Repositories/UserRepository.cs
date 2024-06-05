
using WebStocks.DbStuff.Models;

namespace WebStocks.DbStuff.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(WebDbContext _dbContext) : base(_dbContext) { }

        public User? GetUserByLoginAndPassword(string login, string password)
        {
            return _entities
                .FirstOrDefault(user => user.Login == login && user.Password == password);
        }

        public bool AnyUserWithName(string name)
        {
            return _entities.Any(x => x.Login == name);
        }

        public void SwitchLocal(int userId, string locale)
        {
            var user = GetById(userId);
            user.PreferLocale = locale;
            _dbContext.SaveChanges();
        }
    }
}
