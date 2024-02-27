using Ecommerce.DataAccessLayer.Entities.Misc;

namespace Ecommerce.DataAccessLayer.Repositories.Interfaces
{
    public interface IMiscRepository
    {
        Task<IEnumerable<DropdownItem>> GetCountries();
        Task<DropdownItem> GetCountryById(long countryId);

        Task<IEnumerable<DropdownItem>> GetIndustries();
        Task<DropdownItem> GetIndustryById(long countryId);
    }
}