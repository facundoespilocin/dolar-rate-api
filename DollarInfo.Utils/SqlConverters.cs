namespace DollarInfo.Utils
{
    public class SqlConverters
    {
        public static string ConvertToString(DateTime? dateTime) => dateTime.HasValue ? $"'{dateTime.Value.ToString("yyyy-MM-dd HH:mm:ss")}'" : "NULL";
        public static string ConvertToString(decimal? _decimal) => _decimal.HasValue ? _decimal.ToString().Replace(',', '.') : "NULL";
        public static string ConvertToString(int? _int) => _int.HasValue ? _int.Value.ToString() : "NULL";
    }
}