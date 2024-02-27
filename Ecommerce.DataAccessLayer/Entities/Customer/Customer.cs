using System.Text.Json.Serialization;
using static Ecommerce.Utils.Enums;

namespace Ecommerce.DataAccessLayer.Entities.Product
{
    public class Customer
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        //public DocumentTypes DocumentTypeId { get; set; }
        public long DocumentTypeId { get; set; }
        public string DocumentType { get; set; }
        public long DocumentNumber { get; set; }
        //public GenderTypes GenderId { get; set; }
        public long GenderId { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public long CountryId { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsActive { get; set; } = true;

        [JsonIgnore]
        public long CreatedBy { get; set; }

        [JsonIgnore]
        public DateTime CreatedDate { get; set; }
    }
}