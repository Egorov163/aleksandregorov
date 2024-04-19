namespace WebStocks.DbStuff.Models
{
    public class Stock : BaseModel
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public virtual List<Dividend>? Dividends { get; set; }
    }
}
