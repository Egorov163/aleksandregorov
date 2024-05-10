using Microsoft.EntityFrameworkCore;
using WebStocks.DbStuff.Models;

namespace WebStocks.DbStuff
{
    public class WebDbContext : DbContext
    {       
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Dividend> Dividends { get; set; }
        public DbSet<User> Users { get; set; }

        public WebDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Stock>()
                .HasMany(stock => stock.Dividends)
                .WithOne(dividend => dividend.Stock)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<User>()
                .HasMany(user => user.MyStocks)
                .WithOne(myStocks => myStocks.Owner)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(builder); 
        }
    }
}
