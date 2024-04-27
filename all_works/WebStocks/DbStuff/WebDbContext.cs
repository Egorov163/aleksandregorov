using Microsoft.EntityFrameworkCore;
using WebStocks.DbStuff.Models;

namespace WebStocks.DbStuff
{
    public class WebDbContext : DbContext
    {       
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Dividend> Dividends { get; set; }

        public WebDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Stock>()
                .HasMany(stock => stock.Dividends)
                .WithOne(dividend => dividend.Stock)
                .OnDelete(DeleteBehavior.NoAction);
 

            base.OnModelCreating(builder); 
        }
    }
}
