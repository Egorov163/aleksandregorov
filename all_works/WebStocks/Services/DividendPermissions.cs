using WebStocks.DbStuff.Models;

namespace WebStocks.Services
{
    public class DividendPermissions
    {
        private AuthService _authService;

        public bool IsCurrentUserAdmin => _authService.IsAdmin();

        public DividendPermissions(AuthService authService)
        {
            _authService = authService;
        }

        public bool CanDetele(Dividend dividend)
          => dividend.Owner is null
            || dividend.Owner?.Id == _authService.GetCurrentUserId()
            || IsCurrentUserAdmin;
    }
}
