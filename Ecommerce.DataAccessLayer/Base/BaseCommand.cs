namespace Ecommerce.DataAccessLayer.Base
{
    public class BaseCommand
    {
        public string? Sql { get; set; }
        public object? Parameters { get; set; }
    }
}