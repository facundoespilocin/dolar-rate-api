using Dapper;
using DollarInfo.DAL.Factory;
using DollarInfo.DAL.Models;
using DollarInfo.DAL.Repositories.Interfaces;
using System.Text.Json;

namespace DollarInfo.DAL.Repositories
{
    public class RatesRepository : IRatesRepository
    {
        private IConnectionFactory _factory;

        public RatesRepository(IConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<ExchangeRate>> GetAll()
        {
            using var con = _factory.GetDbConnection;

            var query = @$"SELECT * FROM ExchangeRates;";

            return await con.QueryAsync<ExchangeRate>(query);
        }

        public async Task<IEnumerable<ExchangeRateValues>> GetAllHistoricValues()
        {
            using var con = _factory.GetDbConnection;

            var query = @$"SELECT * FROM ExchangeRatesHistoricValues;";

            return await con.QueryAsync<ExchangeRateValues>(query);
        }

        public async Task<IEnumerable<ExchangeRateValues>> GetLastExchangeRates()
        {
            using var con = _factory.GetDbConnection;

            var query = @$"SELECT JsonValue FROM ExchangeRatesHistoricValues ORDER BY CreatedAt DESC LIMIT 1;";

            string jsonValue = await con.QuerySingleOrDefaultAsync<string>(query);

            if (jsonValue is null)
            {
                return null;
            }

            IEnumerable<ExchangeRateValues> exchangeRatesValues = JsonSerializer.Deserialize<IEnumerable<ExchangeRateValues>>(jsonValue);

            return exchangeRatesValues;
        }

        public async Task InsertExchangeRateValues(IEnumerable<ExchangeRateValues> exchangeRateValues)
        {
            using var con = _factory.GetDbConnection;

            var jsonValue = JsonSerializer.Serialize(exchangeRateValues);

            var query = @$"INSERT INTO ExchangeRatesHistoricValues (JsonValue, CreatedAt) VALUES(@JsonValue, @CreatedAt);";

            await con.ExecuteAsync(query, new
            {
                JsonValue = jsonValue,
                CreatedAt = DateTime.Now
            });
        }

        public async Task<IEnumerable<ExchangeRateValues>> GetFirstRecordOfToday()
        {
            using var con = _factory.GetDbConnection;

            var query = "SELECT JsonValue FROM ExchangeRatesHistoricValues WHERE DATE(CreatedAt) = @Today ORDER BY CreatedAt ASC LIMIT 1;";
            
            string jsonValue = await con.QuerySingleOrDefaultAsync<string>(query, new { DateTime.Today });

            if (jsonValue is null)
            {
                return null;
            }

            IEnumerable<ExchangeRateValues> exchangeRatesValues = JsonSerializer.Deserialize<IEnumerable<ExchangeRateValues>>(jsonValue);

            return exchangeRatesValues;
        }
    }
}