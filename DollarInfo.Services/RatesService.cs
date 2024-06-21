using AutoMapper;
using DollarInfo.DAL.Dtos.Rates;
using DollarInfo.DAL.Models;
using DollarInfo.Services.Helpers;
using DollarInfo.Services.Interfaces;
using DollarInfo.Services.Models;
using DollarInfo.Utils.Settings;
using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.Options;

namespace DollarInfo.Services
{
    public class RatesService : IRatesService
    {
        private readonly HttpClientWrapper _httpClientWrapper;
        private readonly ExternalApiSettings _externalApiSettings;
        private readonly IMapper _mapper;

        public RatesService(
            HttpClientWrapper httpClientWrapper,
            IOptions<ExternalApiSettings> externalApiSettings,
            IMapper mapper)
        {
            _httpClientWrapper = httpClientWrapper;
            _externalApiSettings = externalApiSettings.Value;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<IEnumerable<DollarRatesDto>>> GetAllExchangeRates()
        {
            string url = $"{_externalApiSettings.DolarApi.BaseUrl}/{_externalApiSettings.DolarApi.DolaresUrl}";

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

        public async Task<ServiceResponse<IEnumerable<DollarRatesDto>>> GetAllMarketRates()
        {
            string url = $"{_externalApiSettings.DolarApi.BaseUrl}{_externalApiSettings.DolarApi.CotizacionesUrl}";

            ServiceResponse<IEnumerable<DollarRatesDto>> serviceResponse = new();

            try
            {
                var response = await _httpClientWrapper.GetAsync<IEnumerable<DollarRatesResponse>>(url);

                serviceResponse.Data = _mapper.Map<IEnumerable<DollarRatesDto>>(response.Data);
            }
            catch (Exception ex)
            {
                serviceResponse.AddError(ex);
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<IEnumerable<FixedTermRateDto>>> GetFixedTermRates()
        {
            string url = $"{_externalApiSettings.ArgentinaDatosApi.BaseUrl}{_externalApiSettings.ArgentinaDatosApi.PlazoFijoUrl}";

            ServiceResponse<IEnumerable<FixedTermRateDto>> serviceResponse = new();

            try
            {
                var response = await _httpClientWrapper.GetAsync<IEnumerable<FixedTermRateResponse>>(url);

                serviceResponse.Data = _mapper.Map<IEnumerable<FixedTermRateDto>>(response.Data);
            }
            catch (Exception ex)
            {
                serviceResponse.AddError(ex);
            }

            return serviceResponse;
        }
    }
}