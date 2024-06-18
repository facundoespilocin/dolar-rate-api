using Ecommerce.DataAccessLayer.Entities.Misc;

namespace Ecommerce.DataAccessLayer.Repositories.Interfaces
{
    public interface IMiscRepository
    {
        Task<IEnumerable<DropdownItem>> GetCountries();
    }
}