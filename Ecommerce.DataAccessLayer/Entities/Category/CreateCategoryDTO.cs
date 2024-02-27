namespace Ecommerce.DataAccessLayer.Entities.Category
{
    public class CreateCategoryDTO
    {
        public CategoryDTO Category { get; set; }

        public class CategoryDTO
        {
            public string? Name { get; set; }
            public string? HierarchicalName => string.IsNullOrEmpty(ParentId) ? Name : ParentName + " - " + Name;
            public long Level => string.IsNullOrEmpty(ParentId) ? 0 : 1;
            public bool HasChildren { get; set; } = false;
            public bool IsActive { get; set; } = true;
            public string? ParentId { get; set; }
            public string? ParentName { get; set; }
            public IEnumerable<ChildrenDTO>? Children { get; set; }
            public DateTime CreatedDate { get; set; } = DateTime.Now;
        }

        public class ChildrenDTO
        {
            public string? Name { get; set; }
            public long Level { get; set; }
            public bool HasChildren { get; set; } = false;
        }
    }
}