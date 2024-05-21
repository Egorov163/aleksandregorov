using System.ComponentModel.DataAnnotations;
using WebStocks.Models.ValidationAttributes;

namespace WebStocks.Models
{
    public class AddStockViewModel
    {
        [Required]
        [MinLength(2, ErrorMessage = "Слишком короткое название, не верю")]
        public string Name { get; set; }

        [CheckingForPositiveNumbers]
        public int Price { get; set; }
        public string? LogoUrl { get; set; }
        public DateTime DateBuy { get; set; }
    }
}
