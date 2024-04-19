using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebStocks.Models
{
    public class AddDividendViewModel
    {
        public List<SelectListItem> Stocks { get; set; }
        public int Price { get; set; }

    }
}
