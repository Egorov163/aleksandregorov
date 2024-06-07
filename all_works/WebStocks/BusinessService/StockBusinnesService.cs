using WebStocks.DbStuff.Models;
using WebStocks.DbStuff.Repositories;
using WebStocks.Localizations;
using WebStocks.Models;
using WebStocks.Services;

namespace WebStocks.BusinessService
{
    public class StockBusinnesService
    {
        private StockRepository _stockRepository;
        private StockPermissions _stockPermissions;
        public StockBusinnesService(StockRepository stockRepository, StockPermissions stockPermissions)
        {
            _stockRepository = stockRepository;
            _stockPermissions = stockPermissions;
               
        }
        public List<StockViewModel> GetTop10StocksForMainPage()
        {
            var dbStocks = _stockRepository.Get(10);
            var StockVewModel = dbStocks.
                Select(dbStock => new StockViewModel
                {
                    Id = dbStock.Id,
                    Name = dbStock.Name,
                    Price = dbStock.Price,
                    OwnerName = dbStock.Owner?.Login ?? "Неизвестный",
                    CanDelete = _stockPermissions.CanDetele(dbStock),
                    CanChange = _stockPermissions.CanChange(dbStock)
                })
                .ToList();
            return StockVewModel;
        }
        public List<StockViewModel> GetDeletedStocksForMainPage()
        {
            var dbStocks = _stockRepository.GetDeletedStocks();
            var StockViewModel = dbStocks.
                Select(dbStock => new StockViewModel
                {
                    Id = dbStock.Id,
                    Name = dbStock.Name,
                    Price = dbStock.Price,
                    CanDelete = _stockPermissions.CanDetele(dbStock)
                })
                .ToList();
            return StockViewModel;
        }
        public StockInformationViewModel GetStockInformationForMainPage(int stockId)
        {
            var dbModel = _stockRepository.GetById(stockId);

            var viewModel = new StockInformationViewModel
            {
                Id = dbModel.Id,
                Name = dbModel.Name,
                Price = dbModel.Price,
                LogoUrl = dbModel.LogoUrl,
                DateBuy = dbModel.DateBuy
            };
            return viewModel;
        }
    }
}
