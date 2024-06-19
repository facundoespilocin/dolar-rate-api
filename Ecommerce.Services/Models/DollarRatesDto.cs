namespace Ecommerce.Services.Models
{
    public class DollarRatesDto
    {
        public string Currency { get; set; }
        public string ShortName { get; set; }
        public string Name { get; set; }
        public double PurchasePrice { get; set; }
        public double SalePrice { get; set; }
        public double ExchangeRateIndex { get; set; } = 1;
        public DateTime UpdatedAt { get; set; }
    }
}
