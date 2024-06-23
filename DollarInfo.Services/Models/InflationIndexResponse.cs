using System.Text.Json.Serialization;

namespace DollarInfo.Services.Models
{
    public class InflationIndexResponse
    {
        [JsonPropertyName("fecha")]
        public string Date { get; set; }

        [JsonPropertyName("valor")]
        public double Value { get; set; }
    }
}
