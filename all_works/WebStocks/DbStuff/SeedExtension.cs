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
                SeedUser(serviceScope.ServiceProvider);
            }
        }

        private static void SeedStock(IServiceProvider serviceProvider)
        {
            var stockRepository = serviceProvider.GetService<StockRepository>();
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

        private static void SeedUser(IServiceProvider serviceProvider)
        {
            var userRepository = serviceProvider.GetService<UserRepository>();
            if (!userRepository.AnyUserWithName("admin"))
            {
                var admin = new User
                {
                    Login = "admin",
                    Email = "admin@admin.com",
                    Password = "admin",
                    PreferLocale = "en-US"
                };
                userRepository.Add(admin);
            }

            if (!userRepository.AnyUserWithName("user"))
            {
                var user = new User
                {
                    Login = "user",
                    Email = "user@user.com",
                    Password = "user",
                    PreferLocale = "en-US"
                };
                userRepository.Add(user);
            }
        }
    }
}
