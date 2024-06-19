namespace DollarInfo.DAL.Base
{
    public class BaseCommand
    {
        public string? Sql { get; set; }
        public object? Parameters { get; set; }
    }
}