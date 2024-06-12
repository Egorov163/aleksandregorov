namespace WebStocks.Models.StockApiHelper
{
    public class MethodViewModel
    {
        public string Name { get; set; }
        public List<ParameterViewModel> Parameters { get; set; }
        public MethodType MethodType { get; set; }
    }
}
