namespace DollarInfo.Utils.Settings
{
    public class ExternalApiSettings
    {
        public DolarApiSettingsV1 DolarApi { get; set; }
        public ArgentinaDatosApiSettingsV1 ArgentinaDatosApi { get; set; }
    }

    public class DolarApiSettingsV1
    {
        public string BaseUrl { get; set; }
        public string DolaresUrl { get; set; }
        public string CotizacionesUrl { get; set; }
    }

    public class ArgentinaDatosApiSettingsV1
    {
        public string BaseUrl { get; set; }
        public string PlazoFijoUrl { get; set; }
    }

}