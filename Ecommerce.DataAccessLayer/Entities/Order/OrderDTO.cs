using Ecommerce.DataAccessLayer.Entities.Product;
using static Ecommerce.Utils.Enums;

namespace Ecommerce.DataAccessLayer.Entities.Order
{
    public class OrderDTO
    {
        public long Id { get; set; }
        public long OrganizationId { get; set; }
        public long CustomerId { get; set; }
        public List<ProductDTO> Products { get; set; }
        public long PaymentMethodId { get; set; }
        public double Amount { get; set; }
        public double Discount { get; set; }
        public long Items { get; set; }
        public long Installments { get; set; }
        public long DeliveryType { get; set; }
        public OrderTypes Status => OrderTypes.Initialized;
        public long CreatedBy { get; set; }
        public DateTime CreatedDate => DateTime.Now;
    }
}