namespace WebStocks.Models
{
    public class StockIndexViewModel
    {
        public string UserName { get; set; }
        public bool IsAdmin { get; set; }
        public List<StockViewModel> Stocks { get; set; }

    }
}
