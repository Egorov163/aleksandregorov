using WebStocks.DbStuff.Models;
using WebStocks.Models;

namespace WebStocks.Services
{
    public class StockPermissions
    {
        private AuthService _authService;

        public bool IsCurrentUserAdmin  => _authService.IsAdmin();

        public StockPermissions(AuthService authService)
        {
            _authService = authService;
        }

        public bool CanDetele(Stock stock)
          => stock.Owner is null 
            || stock.Owner?.Id == _authService.GetCurrentUserId()
            || IsCurrentUserAdmin;


    }
}
