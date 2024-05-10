namespace WebStocks.DbStuff.Models
{
    public class Stock : BaseModel
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public bool IsDeleted { get; set; }
        public string? LogoUrl { get; set; }
        public DateTime DateBuy { get; set; }
        public virtual List<Dividend>? Dividends { get; set; }        
        public virtual User? Owner { get; set; }
    }
}
