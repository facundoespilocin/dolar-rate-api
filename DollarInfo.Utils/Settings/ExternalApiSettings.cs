namespace DollarInfo.Utils.Settings
{
    public class ExternalApiSettings
    {
        public DolarApiSettings DolarApi { get; set; }
        public ArgentinaDatosApiSettings ArgentinaDatosApi { get; set; }
    }

    public class DolarApiSettings
    {
        public string BaseUrl { get; set; }
        public string DolaresUrl { get; set; }
        public string CotizacionesUrl { get; set; }
    }

    public class ArgentinaDatosApiSettings
    {
        public string BaseUrl { get; set; }
        public string PlazoFijoUrl { get; set; }
        public IndicesUrlSettings IndicesUrl { get; set; }
    }

    public class IndicesUrlSettings
    {
        public string BaseUrl { get; set; }
        public string MensualUrl { get; set; }
        public string InteranualUrl { get; set; }
        public string UvaUrl { get; set; }
    }
}