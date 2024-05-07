using Microsoft.Extensions.DependencyInjection;
using WebStocks.DbStuff.Models;
using WebStocks.DbStuff.Repositories;

namespace WebStocks.DbStuff
{
    public static class SeedExtension
    {
        public static void Seed(WebApplication app)
        {

            using (var serviceScope = app.Services.CreateScope())
            {
                SeedStock(serviceScope.ServiceProvider);
            }

        }

        private static void SeedStock(IServiceProvider di)
        {
            var stockRepository = di.GetService<StockRepository>();
            if (!stockRepository.Any())
            {
                var stock = new Stock
                {
                    Name = "Coca-Cola",
                    Price = 3000,
                    IsDeleted = false,
                    DateBuy = DateTime.Now
                };
                stockRepository.Add(stock);
            }
        }
    }
}
