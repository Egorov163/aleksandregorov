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

        public User? GetCurrentUser()
        {
            var id = GetCurrentUserId();
            if (id == null)
            {
                return null;
            }

            return _userRepository.GetById(id.Value);
        }

        public int? GetCurrentUserId()
        {
            var idStr = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id")?.Value;
            if (idStr == null)
            {
                return null;
            }

            var id = int.Parse(idStr);
            return id;
        }

        public string? GetCurrentUserName()
        {
            return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "name")?.Value;
        }

        public bool IsAdmin()
        {
            return GetCurrentUserName() == "admin";
        }
    }
}
