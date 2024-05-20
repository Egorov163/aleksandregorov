namespace WebStocks.DbStuff.Models
{
    public class User : BaseModel
    {
        public string? Login { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? AvatarUrl { get; set; }
        public virtual List<Stock> MyStocks { get; set; }
        public virtual List<Dividend> MyDividends { get; set; }

    }
}
