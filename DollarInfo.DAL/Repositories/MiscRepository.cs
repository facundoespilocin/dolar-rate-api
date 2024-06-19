using Dapper;
using DollarInfo.DAL.Entities.Misc;
using DollarInfo.DAL.Factory;
using DollarInfo.DAL.Repositories.Interfaces;

namespace DollarInfo.DAL.Repositories
{
    public class MiscRepository : IMiscRepository
    {
        private IConnectionFactory _factory;

        public MiscRepository(IConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<DropdownItem>> GetCountries()
        {
            using var con = _factory.GetDbConnection;

            var query = "SELECT * FROM Countries;";

            return await con.QueryAsync<DropdownItem>(query);
        }
    }
}