using static Ecommerce.Utils.Enums;

namespace Ecommerce.DataAccessLayer.Entities.Order
{
    public class RemoveOrderItemDTO
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public long OrganizationId { get; set; }
        public long Items { get; set; }
        public double Amount { get; set; }
        public OrderTypes Status => OrderTypes.Initialized;
        public long ModifiedBy { get; set; }
        public DateTime CreatedDate => DateTime.Now;
    }
}