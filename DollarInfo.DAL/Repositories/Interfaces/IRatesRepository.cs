using DollarInfo.DAL.Models;

namespace DollarInfo.DAL.Repositories.Interfaces
{
    public interface IRatesRepository
    {
        Task<IEnumerable<ExchangeRate>> GetAll();
        Task<IEnumerable<ExchangeRateValues>> GetAllHistoricValues();
        Task<IEnumerable<ExchangeRateValues>> GetLastExchangeRates();
        Task InsertExchangeRateValues(IEnumerable<ExchangeRateValues> exchangeRateValues);
        Task<IEnumerable<ExchangeRateValues>> GetFirstRecordOfToday();
    }
}