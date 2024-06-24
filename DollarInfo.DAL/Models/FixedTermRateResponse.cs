using System.Text.Json.Serialization;

namespace DollarInfo.Services.Models
{
    public class FixedTermRateResponse
    {
        [JsonPropertyName("entidad")]
        public string Entity { get; set; }

        [JsonPropertyName("logo")]
        public string Logo { get; set; }

        [JsonPropertyName("tnaClientes")]
        public double ClientsTna { get; set; }

        [JsonPropertyName("tnaNoClientes")]
        public object NoClientsTna { get; set; }

        [JsonPropertyName("enlace")]
        public string Link { get; set; }
    }
}
