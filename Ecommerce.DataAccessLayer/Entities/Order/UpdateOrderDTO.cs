using Ecommerce.DataAccessLayer.Entities.Product;

namespace Ecommerce.DataAccessLayer.Entities.Order
{
    public class UpdateOrderDTO : CreateOrderDTO
    {
        public IEnumerable<ProductDTO> Products { get; set; }
    }
}