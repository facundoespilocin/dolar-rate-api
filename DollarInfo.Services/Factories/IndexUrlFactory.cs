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
            switch (inflationIndexType)
            {
                case InflationIndexTypes.Monthly:
                    return $"{_settings.ArgentinaDatosApi.BaseUrl}{_settings.ArgentinaDatosApi.IndicesUrl.BaseUrl}{_settings.ArgentinaDatosApi.IndicesUrl.MensualUrl}";
                case InflationIndexTypes.YearOnYear:
                    return $"{_settings.ArgentinaDatosApi.BaseUrl}{_settings.ArgentinaDatosApi.IndicesUrl.BaseUrl}{_settings.ArgentinaDatosApi.IndicesUrl.InteranualUrl}";
                case InflationIndexTypes.Uva:
                    return $"{_settings.ArgentinaDatosApi.BaseUrl}{_settings.ArgentinaDatosApi.IndicesUrl.BaseUrl}{_settings.ArgentinaDatosApi.IndicesUrl.UvaUrl}";

                default:
                    throw new ArgumentException("Unsupported inflation index type.");
            }
        }
    }
}
