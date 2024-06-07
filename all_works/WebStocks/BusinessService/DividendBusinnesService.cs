using WebStocks.DbStuff.Repositories;
using WebStocks.Models;
using WebStocks.Services;

namespace WebStocks.BusinessService
{
    public class DividendBusinnesService
    {
        private DividendRepository _dividendRepository;
        private DividendPermissions _dividendPermissions;
        public DividendBusinnesService(DividendRepository dividendRepository, DividendPermissions dividendPermissions)
        {
            _dividendRepository = dividendRepository;
            _dividendPermissions = dividendPermissions;
               
        }
        public List<DividendViewModel> GetTop10DividendsForMainPage()
        {
            var dbDividends = _dividendRepository.Get(10);

            var DividendViewModel = dbDividends.Select(dbDividends => new DividendViewModel
            {
                Id = dbDividends.Id,
                NameStock = dbDividends.Stock.Name,
                Price = dbDividends.Price,
                OwnerName = dbDividends.Owner?.Login ?? "Неизвестный",
                CanDelete = _dividendPermissions.CanDetele(dbDividends)

            })
                .ToList();
            return DividendViewModel;
        }

    }
}
