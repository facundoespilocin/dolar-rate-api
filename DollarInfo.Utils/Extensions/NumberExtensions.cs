namespace DollarInfo.Utils.Extensions
{
    public static class NumberExtensions
    {
        public static double FormatDecimal(object value)
        {
            if (value is null)
            {
                return 0;
            }

            if (value is double doubleValue)
            {
                return Math.Round(doubleValue, 2);
            }

            if (double.TryParse(value.ToString(), out double parsedValue))
            {
                return Math.Round(parsedValue, 2);
            }

            return 0;
        }
    }
}