using AutoMapper;
using DollarInfo.DAL.Dtos.Rates;
using DollarInfo.DAL.Models;
using DollarInfo.DAL.Repositories.Interfaces;
using DollarInfo.Services.Helpers;
using DollarInfo.Services.Interfaces;
using DollarInfo.Services.Models;
using DollarInfo.Utils.Settings;
using Microsoft.Extensions.Options;

namespace DollarInfo.Services
{
    public class RatesService : IRatesService
    {
        private readonly HttpClientWrapper _httpClientWrapper;
        private readonly ExternalApiSettings _externalApiSettings;
        private readonly IRatesRepository _ratesRepository;
        private readonly IMapper _mapper;

        public RatesService(
            HttpClientWrapper httpClientWrapper,
            IOptions<ExternalApiSettings> externalApiSettings,
            IRatesRepository ratesRepository,
            IMapper mapper)
        {
            _httpClientWrapper = httpClientWrapper;
            _externalApiSettings = externalApiSettings.Value;
            _ratesRepository = ratesRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<IEnumerable<ExchangeRateValues>>> ProcessExchangeRates()
        {
            try
            {
                ServiceResponse<IEnumerable<ExchangeRateValues>> serviceResponse = new();

                var currentExchangeRates = await GetAllExchangeRates();

                if (!currentExchangeRates.Data.Any())
                {
                    return serviceResponse;
                }

                serviceResponse.Data = _mapper.Map<IEnumerable<ExchangeRateValues>>(currentExchangeRates.Data);

                IEnumerable<ExchangeRateValues> lastExchangeRates = await _ratesRepository.GetLastExchangeRates();

                await ExchangeRatesHasDifferences(serviceResponse.Data, lastExchangeRates);

                IEnumerable<ExchangeRateValues> firstRecordOfToday = await _ratesRepository.GetFirstRecordOfToday();

                CalculateVariation(serviceResponse.Data, firstRecordOfToday);

                return serviceResponse;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving Exchange Rates: {ex.Message}");
            }
        }

        public async Task<ServiceResponse<IEnumerable<DollarRatesResponse>>> GetAllExchangeRates()
        {
            string url = $"{_externalApiSettings.DolarApi.BaseUrl}/{_externalApiSettings.DolarApi.DolaresUrl}";

            ServiceResponse<IEnumerable<DollarRatesResponse>> serviceResponse = new();

            try
            {
                var response = await _httpClientWrapper.GetAsync<IEnumerable<DollarRatesResponse>>(url);

                serviceResponse.Data = _mapper.Map<IEnumerable<DollarRatesResponse>>(response.Data);

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

        private async Task<bool> ExchangeRatesHasDifferences(IEnumerable<ExchangeRateValues> currentExchangeRates, IEnumerable<ExchangeRateValues> lastExchangeRates)
        {
            if (lastExchangeRates is null || !lastExchangeRates.Any())
            {
                await _ratesRepository.InsertExchangeRateValues(currentExchangeRates);

                return true;
            }

            var currentDictionary = currentExchangeRates.ToDictionary(x => x.Id);

            var lastDictionary = lastExchangeRates.ToDictionary(x => x.Id);

            foreach (var kvp in currentDictionary)
            {
                if (lastDictionary.TryGetValue(kvp.Key, out var lastValue))
                {
                    var currentValue = kvp.Value;

                    if (currentValue.PurchasePrice != lastValue.PurchasePrice || currentValue.SalePrice != lastValue.SalePrice)
                    {
                        await _ratesRepository.InsertExchangeRateValues(currentExchangeRates);

                        return true;
                    }
                }
            }

            return false;
        }

        private static void CalculateVariation(IEnumerable<ExchangeRateValues> currentRates, IEnumerable<ExchangeRateValues> firstRecordOfToday)
        {
            foreach (var rate in currentRates)
            {
                var lastRate = firstRecordOfToday.FirstOrDefault(lr => lr.Id == rate.Id);

                if (lastRate is not null)
                {
                    rate.CalculateVariation(lastRate.SalePrice, rate.SalePrice);
                }
            }
        }
    }
}