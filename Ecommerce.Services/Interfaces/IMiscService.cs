using Ecommerce.DataAccessLayer.Entities.Misc;

namespace Ecommerce.Services.Interfaces
{
    public interface IDolarRateService
    {
        Task<IEnumerable<DropdownItem>> GetAllExchangeRates();
    }
}