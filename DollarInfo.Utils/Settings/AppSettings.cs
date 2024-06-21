﻿namespace DollarInfo.Utils.Settings
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
    }
}