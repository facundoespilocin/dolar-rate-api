using DollarInfo.DAL.Entities.Misc;
using DollarInfo.DAL.Models;
using DollarInfo.Services.Models;

namespace DollarInfo.Services.Interfaces
{
    public interface IDollarRateService
    {
        Task<ServiceResponse<IEnumerable<DollarRatesDto>>> GetAllExchangeRates();
    }
}