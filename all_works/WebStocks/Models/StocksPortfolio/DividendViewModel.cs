namespace WebStocks.Models
{
    public class DividendViewModel
    {
        public int Id { get; set; }
        public string NameStock { get; set; }
        public int Price { get; set; }
        public bool CanDelete { get; set; }
        public string OwnerName { get; set; }
    }
}
