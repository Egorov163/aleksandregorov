namespace WebStocks.Models
{
    public class StockViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string OwnerName { get; set; }
        public bool CanDelete { get; set; }       
    }
}
