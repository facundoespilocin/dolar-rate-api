using AutoMapper;
using DollarInfo.DAL.Dtos.Indexes;
using DollarInfo.DAL.Dtos.Rates;
using DollarInfo.DAL.Models;
using DollarInfo.Services.Helpers;
using DollarInfo.Services.Interfaces;
using DollarInfo.Services.Models;
using DollarInfo.Utils;
using DollarInfo.Utils.Settings;
using Microsoft.Extensions.Options;
using static DollarInfo.Utils.Enums;

namespace DollarInfo.Services
{
    public class IndexesService : IIndexesService
    {
        private readonly HttpClientWrapper _httpClientWrapper;
        private readonly ExternalApiSettings _settings;
        private readonly IMapper _mapper;

        public IndexesService(
            HttpClientWrapper httpClientWrapper,
            IOptions<ExternalApiSettings> settings,
            IMapper mapper)
        {
            _httpClientWrapper = httpClientWrapper;
            _settings = settings.Value;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<IEnumerable<DollarRatesDto>>> GetAllExchangeRates()
        {
            string url = $"{_settings.DolarApi.BaseUrl}/{_settings.DolarApi.DolaresUrl}";

            ServiceResponse<IEnumerable<DollarRatesDto>> serviceResponse = new();

            try
            {
                var response = await _httpClientWrapper.GetAsync<IEnumerable<DollarRatesResponse>>(url);

                serviceResponse.Data = _mapper.Map<IEnumerable<DollarRatesDto>>(response.Data);

                //_logger.LogPayrInfo("Description.", payload: serviceResponse.Data);
            }
            catch (Exception ex)
            {
                serviceResponse.AddError(ex);
                //_logger.LogPayrException(EventCode.UNEXPECTED_EXCEPTION, "Description.", ex, alias);
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<IEnumerable<InflationIndexDto>>> GetYearOnYearInflationIndexes()
        {
            string url = $"{_settings.ArgentinaDatosApi.BaseUrl}{_settings.ArgentinaDatosApi.IndicesUrl.BaseUrl}{_settings.ArgentinaDatosApi.IndicesUrl.InteranualUrl}";

            ServiceResponse<IEnumerable<InflationIndexDto>> serviceResponse = new();

            try
            {
                var response = await _httpClientWrapper.GetAsync<IEnumerable<InflationIndexResponse>>(url);

                serviceResponse.Data = _mapper.Map<IEnumerable<InflationIndexDto>>(response.Data);
            }
            catch (Exception ex)
            {
                serviceResponse.AddError(ex);
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<IEnumerable<InflationIndexDto>>> GetInflationIndexes(InflationIndexTypes inflationIndexType)
        {
            string url = @$"{_settings.ArgentinaDatosApi.BaseUrl}{_settings.ArgentinaDatosApi.IndicesUrl.BaseUrl}{(inflationIndexType == InflationIndexTypes.Monthly ? _settings.ArgentinaDatosApi.IndicesUrl.MensualUrl : _settings.ArgentinaDatosApi.IndicesUrl.InteranualUrl)}";

            ServiceResponse<IEnumerable<InflationIndexDto>> serviceResponse = new();

            try
            {
                var response = await _httpClientWrapper.GetAsync<IEnumerable<InflationIndexResponse>>(url);

                serviceResponse.Data = _mapper.Map<IEnumerable<InflationIndexDto>>(response.Data);
            }
            catch (Exception ex)
            {
                serviceResponse.AddError(ex);
            }

            return serviceResponse;
        }
    }
}