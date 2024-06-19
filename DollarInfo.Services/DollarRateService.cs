using AutoMapper;
using DollarInfo.DAL.Models;
using DollarInfo.Services.Helpers;
using DollarInfo.Services.Interfaces;
using DollarInfo.Services.Models;
using DollarInfo.Utils.Settings;
using Microsoft.Extensions.Options;

namespace DollarInfo.Services
{
    public class DollarRateService : IDollarRateService
    {
        private readonly HttpClientWrapper _httpClientWrapper;
        private readonly DolarApiSettings _dolarApiSettings;
        private readonly IMapper _mapper;

        public DollarRateService(
            HttpClientWrapper httpClientWrapper,
            IOptions<DolarApiSettings> dolarApiSettings, 
            IMapper mapper)
        {
            _httpClientWrapper = httpClientWrapper;
            _dolarApiSettings = dolarApiSettings.Value;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<IEnumerable<DollarRatesDto>>> GetAllExchangeRates()
        {
            string url = $"{_dolarApiSettings.BaseUrl}/{_dolarApiSettings.DolarUrl}";

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
    }
}