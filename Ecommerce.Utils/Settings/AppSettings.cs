namespace Ecommerce.Utils.Settings
{
    public class AppSettings
    {
        public ExternalApiSettings ExternalApi { get; set; }

        public class ExternalApiSettings
        {
            public DolarApiSetings DolarApi { get; set; }

            public class DolarApiSetings
            {
                public string BaseUrl { get; set; }
                public string DolarUrl { get; set; }
                public string CotizacionesUrl { get; set; }
            }
        }
    }
}