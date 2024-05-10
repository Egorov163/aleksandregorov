using WebStocks.DbStuff.Models;
using WebStocks.DbStuff.Repositories;

namespace WebStocks.Services
{
    public class AuthService
    {
        private UserRepository _userRepository;
        private IHttpContextAccessor _httpContextAccessor;

        public AuthService(UserRepository userRepository, 
            IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public User GetCurrentUser()
        {
            var idStr = _httpContextAccessor.HttpContext.User.Claims.First(x => x.Type == "id").Value;
            var id = int.Parse(idStr);
            return _userRepository.GetById(id);
        }
    }
}
