using Dapper;
using Ecommerce.DataAccessLayer.Entities.Misc;
using Ecommerce.DataAccessLayer.Factory;
using Ecommerce.DataAccessLayer.Repositories.Interfaces;

namespace Ecommerce.DataAccessLayer.Repositories
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

        public async Task<DropdownItem> GetCountryById(long countryId)
        {
            using var con = _factory.GetDbConnection;

            var query = $"SELECT * FROM Countries WHERE Id = {countryId};";

            return await con.QueryFirstOrDefaultAsync<DropdownItem>(query);
        }

        public async Task<IEnumerable<DropdownItem>> GetIndustries()
        {
            using var con = _factory.GetDbConnection;

            var query = "SELECT * FROM Industries;";

            return await con.QueryAsync<DropdownItem>(query);
        }

        public async Task<DropdownItem> GetIndustryById(long industryId)
        {
            using var con = _factory.GetDbConnection;

            var query = $"SELECT * FROM Industries WHERE Id = {industryId};";

            return await con.QueryFirstOrDefaultAsync<DropdownItem>(query);
        }
    }
}