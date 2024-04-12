using Microsoft.EntityFrameworkCore;
using WebStocks.DbStuff.Models;

namespace WebStocks.DbStuff
{
    public class WebDbContext : DbContext
    {       
        public DbSet<Stock> Stocks { get; set; }


        public WebDbContext(DbContextOptions options) : base(options) { }
    }
}
