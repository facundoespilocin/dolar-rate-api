namespace DollarInfo.Utils.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class CountryDetailsAttribute : Attribute
    {
        public string ISO { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }

        public CountryDetailsAttribute(string iso, double latitude, double longitude)
        {
            ISO = iso;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}