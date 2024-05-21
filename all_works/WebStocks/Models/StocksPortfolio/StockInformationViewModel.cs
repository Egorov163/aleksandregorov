namespace WebStocks.Models
{
    public class StockInformationViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string? LogoUrl { get; set; }
        public DateTime DateBuy { get; set; }
        public string UserName { get; set; }
    }
}
