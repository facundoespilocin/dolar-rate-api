using Ecommerce.DataAccessLayer.Entities.Misc;
using Ecommerce.DataAccessLayer.Models;
using Ecommerce.Services.Models;

namespace Ecommerce.Services.Interfaces
{
    public interface IDollarRateService
    {
        Task<ServiceResponse<IEnumerable<DollarRatesDto>>> GetAllExchangeRates();
    }
}