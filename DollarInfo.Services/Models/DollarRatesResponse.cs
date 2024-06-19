using System.Text.Json.Serialization;

namespace DollarInfo.Services.Models
{
    public class DollarRatesResponse
    {
        [JsonPropertyName("moneda")]
        public string Currency { get; set; }

        [JsonPropertyName("casa")]
        public string ShortName { get; set; }

        [JsonPropertyName("nombre")]
        public string Name { get; set; }

        [JsonPropertyName("compra")]
        public double PurchasePrice { get; set; }

        [JsonPropertyName("venta")]
        public double SalePrice { get; set; }

        [JsonPropertyName("fechaActualizacion")]
        public DateTime UpdatedAt { get; set; }
    }
}
