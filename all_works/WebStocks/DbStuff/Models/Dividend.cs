namespace WebStocks.DbStuff.Models
{
    public class Dividend : BaseModel
    {
        public int Price { get; set; }
        public virtual Stock Stock { get; set; }
        public virtual User? Owner { get; set; }
    }
}
