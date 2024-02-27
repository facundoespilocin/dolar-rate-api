using Ecommerce.DataAccessLayer.Entities.Product;
using System.Text.Json.Serialization;

namespace Ecommerce.DataAccessLayer.Entities.Cart
{
    public class Cart
    {
        public long Id { get; set; }
        public List<ProductDTO> Products { get; set; }
        public long Items { get; set; }
        public long Status { get; set; }
        public double Total { get; set; }

        [JsonIgnore]
        public long CreatedBy { get; set; }

        [JsonIgnore]
        public DateTime CreatedDate { get; set; }
    }
}