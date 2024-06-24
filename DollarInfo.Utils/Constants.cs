namespace DollarInfo.Utils
{
    public static class Constants
    {
        public const string EmailFrom = "dolarinformation@gmail.com";
        public const string Subject = "[Bug] - Reporte de Bug";

        public class DateFormats
        {
            public class Dash
            {
                public const string Default = "yyyy-MM-dd";
            }

            public class Slashes
            {
                public const string DayFirst = "dd/MM/yyyy";
                public const string MonthFirst = "MM/dd/yyyy HH:mm:ss";
            }
        }
    }
}