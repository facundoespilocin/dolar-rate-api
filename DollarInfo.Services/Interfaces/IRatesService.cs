using DollarInfo.DAL.Dtos.Rates;
using DollarInfo.DAL.Models;
using DollarInfo.Services.Models;

namespace DollarInfo.Services.Interfaces
{
    public interface IRatesService
    {
        Task<ServiceResponse<IEnumerable<ExchangeRateValues>>> ProcessExchangeRates();
        Task<ServiceResponse<IEnumerable<DollarRatesResponse>>> GetAllExchangeRates();
        Task<ServiceResponse<IEnumerable<DollarRatesDto>>> GetAllExchangeRatesTemp();
        Task<ServiceResponse<IEnumerable<DollarRatesDto>>> GetAllMarketRates();
        Task<ServiceResponse<IEnumerable<FixedTermRateDto>>> GetFixedTermRates();
    }
}