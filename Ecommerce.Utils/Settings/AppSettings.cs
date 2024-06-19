namespace Ecommerce.Utils.Settings
{
    public class AppSettings
    {
        public AppSettings()
        {
            Secret = "BandOfCodersSymmetricEncryptionKey";
        }

        public string BaseUrl { get; set; }
        public string Secret { get; set; }
        public string EmailFrom { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUser { get; set; }
        public string SmtpPass { get; set; }

        public ExternalApiSettings ExternalApi { get; set; }

        public class ExternalApiSettings
        {
            public DolarApiSettings DolarApi { get; set; }

            public class DolarApiSettings
            {
                public string BaseUrl { get; set; }
                public string DolarUrl { get; set; }
                public string CotizacionesUrl { get; set; }
            }
        }
    }
}