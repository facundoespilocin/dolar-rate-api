using DollarInfo.DAL.Dtos.Rates;
using DollarInfo.DAL.Models;

namespace DollarInfo.Services.Interfaces
{
    public interface IRatesService
    {
        Task<ServiceResponse<IEnumerable<DollarRatesDto>>> GetAllExchangeRates();
        Task<ServiceResponse<IEnumerable<DollarRatesDto>>> GetAllMarketRates();
        Task<ServiceResponse<IEnumerable<FixedTermRateDto>>> GetFixedTermRates();
    }
}