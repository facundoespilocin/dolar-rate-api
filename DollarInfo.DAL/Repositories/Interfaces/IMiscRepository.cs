using DollarInfo.DAL.Entities.Misc;

namespace DollarInfo.DAL.Repositories.Interfaces
{
    public interface IMiscRepository
    {
        Task<IEnumerable<DropdownItem>> GetCountries();
    }
}