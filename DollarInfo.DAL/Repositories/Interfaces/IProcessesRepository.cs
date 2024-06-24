using DollarInfo.Services.Models;

namespace DollarInfo.DAL.Repositories.Interfaces
{
    public interface IProcessesRepository
    {
        Task InsertInflationIndex(IEnumerable<DollarRatesResponse> dollarRates);
    }
}