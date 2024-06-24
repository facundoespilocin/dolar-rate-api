using Dapper;
using DollarInfo.DAL.Dtos.Indexes;
using DollarInfo.DAL.Factory;
using DollarInfo.DAL.Repositories.Interfaces;
using DollarInfo.Utils.Extensions;
using System.Runtime.Serialization;
using static DollarInfo.Utils.Enums;

namespace DollarInfo.DAL.Repositories
{
    public class ProcessesRepository : IProcessesRepository
    {
        private IConnectionFactory _factory;

        public ProcessesRepository(IConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<InflationIndexDto>> GetAll(InflationIndexTypes inflationIndexType)
        {
            using var con = _factory.GetDbConnection;

            string? table = inflationIndexType.GetValueMember<EnumMemberAttribute>().Value;

            var query = @$"SELECT * FROM {table} ORDER BY Date DESC;";

            return await con.QueryAsync<InflationIndexDto>(query);
        }

        public async Task<InflationIndexDto> GetLastInflationIndex(InflationIndexTypes inflationIndexType)
        {
            using var con = _factory.GetDbConnection;

            string? table = inflationIndexType.GetValueMember<EnumMemberAttribute>().Value;

            var query = @$"SELECT * FROM {table} ORDER BY Date DESC LIMIT 1;";

            return await con.QuerySingleOrDefaultAsync<InflationIndexDto>(query);
        }

        public async Task InsertInflationIndex(InflationIndexDto inflationIndex, InflationIndexTypes inflationIndexType)
        {
            using var con = _factory.GetDbConnection;

            string? table = inflationIndexType.GetValueMember<EnumMemberAttribute>().Value;

            var query = @$"INSERT INTO {table} (Date, Value) VALUES (@Date, @Value);";

            await con.ExecuteAsync(query, inflationIndex);
        }

        public async Task InsertBulkInflationIndex(IEnumerable<InflationIndexDto> inflationIndexes, InflationIndexTypes inflationIndexType)
        {
            using var con = _factory.GetDbConnection;
            
            string? table = inflationIndexType.GetValueMember<EnumMemberAttribute>().Value;

            var query = @$"INSERT INTO {table} (Date, Value) VALUES (@Date, @Value);";

            foreach (var item in inflationIndexes)
            {
                await con.ExecuteAsync(query, item);
            }
        }
    }
}