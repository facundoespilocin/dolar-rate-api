namespace DollarInfo.DAL.Models
{
    public class ExchangeRateValues
    {
        public long Id { get; set; }
        public string Currency { get; set; }
        public string ShortName { get; set; }
        public string Name { get; set; }
        public double PurchasePrice { get; set; }
        public double SalePrice { get; set; }
        public double ExchangeRateIndex { get; set; } = 0;
        public DateTime UpdatedAt { get; set; }

        public ExchangeRateValues()
        {
            PurchasePrice = Math.Round(PurchasePrice, 2);
            SalePrice = Math.Round(SalePrice, 2);
            ExchangeRateIndex = Math.Round(ExchangeRateIndex, 2);
        }

        public void CalculateVariation(double previousValue, double currentValue)
        {
            if (previousValue == 0)
            {
                throw new ArgumentException("Previos Value cannot be zero.");
            }

            ExchangeRateIndex = Math.Round(((currentValue - previousValue) / previousValue) * 100, 2);
        }
    }
}