using Ecommerce.DataAccessLayer.Entities.Misc;

namespace Ecommerce.Services.Interfaces
{
    public interface IMiscService
    {
        Task<IEnumerable<DropdownItem>> GetCountries();
        Task<DropdownItem> GetCountryById(long countryId);
        
        Task<IEnumerable<DropdownItem>> GetIndustries();
        Task<DropdownItem> GetIndustryById(long countryId);
    }
}