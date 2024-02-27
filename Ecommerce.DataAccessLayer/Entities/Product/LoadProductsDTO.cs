using System.Text.Json.Serialization;

namespace Ecommerce.DataAccessLayer.Entities.Product
{
    public class LoadProductsDTO
    {
        public IEnumerable<Product> Products { get; set; }
    }
}