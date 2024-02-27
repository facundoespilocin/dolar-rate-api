using System.Text.Json.Serialization;

namespace Ecommerce.DataAccessLayer.Entities.Category
{
    public class Category
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? HierarchicalName { get; set; }
        public bool IsActive { get; set; } = true;
        public int Level { get; set; }
        public bool HasChildren { get; set; } = false;
        public string? Ancestry { get; set; }
        public long ParentId { get; set; }


        [JsonIgnore]
        public long CreatedBy { get; set; }

        [JsonIgnore]
        public DateTime CreatedDate { get; set; }
    }
}