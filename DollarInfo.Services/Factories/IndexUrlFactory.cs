using DollarInfo.Utils.Settings;
using Microsoft.Extensions.Options;
using static DollarInfo.Utils.Enums;

namespace DollarInfo.Services.Factories
{
    public class IndexUrlFactory
    {
        private readonly ExternalApiSettings _settings;

        public IndexUrlFactory(IOptions<ExternalApiSettings> settings)
        {
            _settings = settings.Value;
        }

        public string GetIndexUrl(InflationIndexTypes inflationIndexType)
        {
            return inflationIndexType switch
            {
                InflationIndexTypes.Monthly => $"{_settings.ArgentinaDatosApi.BaseUrl}{_settings.ArgentinaDatosApi.IndicesUrl.BaseUrl}{_settings.ArgentinaDatosApi.IndicesUrl.MensualUrl}",
                InflationIndexTypes.YearOnYear => $"{_settings.ArgentinaDatosApi.BaseUrl}{_settings.ArgentinaDatosApi.IndicesUrl.BaseUrl}{_settings.ArgentinaDatosApi.IndicesUrl.InteranualUrl}",
                InflationIndexTypes.Uva => $"{_settings.ArgentinaDatosApi.BaseUrl}{_settings.ArgentinaDatosApi.IndicesUrl.BaseUrl}{_settings.ArgentinaDatosApi.IndicesUrl.UvaUrl}",
                _ => throw new ArgumentException("Unsupported inflation index type."),
            };
        }
    }
}
