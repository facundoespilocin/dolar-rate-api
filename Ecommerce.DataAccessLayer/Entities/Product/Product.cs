using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ecommerce.DataAccessLayer.Entities.Product
{
    public class Product
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        public long Quantity { get; set; }
        public bool IsActive { get; set; } = true;
        public long CategoryId { get; set; }

        [JsonIgnore]
        public long BrandId { get; set; }

        public string? Tags { get; set; }
        public string? MainImageUrl { get; set; }

        [JsonIgnore]
        public string? ImagesUrlList { get; set; }

        public IEnumerable<string> ImagesUrl { get; set; }

        [JsonIgnore]
        public long CreatedBy { get; set; }

        [JsonIgnore]
        public DateTime CreatedDate { get; set; }
    }

    public class ProductDTO : Product
    {
        public string? CategoryName { get; set; }
    }

    public class ProductDetailDTO : ProductDTO
    {
        public string? Description { get; set; }
        public IEnumerable<string> PaymentMethods { get; set; }

        [JsonIgnore]
        public string? DeliveriesIds { get; set; }

        [JsonIgnore]
        public string SizesList { get; set; }

        [JsonIgnore]
        public string PaymentMethodsList { get; set; }

        public IEnumerable<string> Sizes { get; set; }
        public double ShippingCost { get; set; }
        public double WithdrawalCost { get; set; }
        public IEnumerable<string> Deliveries { get; set; }
        public long Installments => 3;
        public double PriceInInstallments => Price > 0 ? Math.Round(Price / Installments, 2) : 0;
        public bool HasSizes => Sizes is not null && Sizes.Any();
    }
}