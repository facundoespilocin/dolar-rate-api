using static Ecommerce.Utils.Enums;

namespace Ecommerce.DataAccessLayer.Models
{
    public class SearchRequest
    {
        public long CurrentPage { get; set; } = 1;
        public long PageSize { get; set; } = 15;
        public string? Query { get; set; }
        //public string FieldName { get; set; }
        //public OrderType OrderType { get; set; } = OrderType.Desc;
        public string? SearchBy { get; set; }
        public StatusTypes Status { get; set; } = StatusTypes.All;
    }
}