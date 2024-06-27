namespace DollarInfo.DAL.Models
{
    public class ExchangeRate
    {
        public long Id { get; set; }
        public string Currency { get; set; }
        public string ShortName { get; set; }
        public string Name { get; set; }
        public double ExchangeRateIndex { get; set; } = 1;
        public DateTime UpdatedAt { get; set; }
    }
}