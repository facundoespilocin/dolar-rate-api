using Dapper;
using Ecommerce.DataAccessLayer.Entities.Product;
using Ecommerce.DataAccessLayer.Factory;
using Ecommerce.DataAccessLayer.Models;
using Ecommerce.DataAccessLayer.Repositories.Interfaces;

namespace Ecommerce.DataAccessLayer.Repositories
{
    public class CustomersRepository : ICustomersRepository
    {
        private IConnectionFactory _factory;

        public CustomersRepository(IConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<Customer>> GetAll(SearchRequest request)
        {
            using var con = _factory.GetDbConnection;

            List<string> whereClause = new() { };

            if (request.Status != Utils.Enums.StatusTypes.All)
            {
                whereClause.Add($"c.IsActive = {(int)request.Status}");
            }

            if (!string.IsNullOrEmpty(request.Query) && !string.IsNullOrEmpty(request.SearchBy))
            {
                whereClause.Add($"UPPER({request.SearchBy}) LIKE UPPER('%{request.Query}%')");
            }

            var where = (whereClause.Any() ? " WHERE " : "") + string.Join(" AND ", whereClause);

            string select = $@"SELECT c.*, g.Name AS Gender, dt.Name AS DocumentType, co.Name AS Country FROM Customers c
                               INNER JOIN Genders g ON c.GenderId = g.Id
                               INNER JOIN DocumentTypes dt ON c.DocumentTypeId = dt.Id
                               INNER JOIN Countries co ON c.CountryId = co.Id ";

            var productsQuery = $@"{select} {where};";

            var customers = await con.QueryAsync<Customer>(productsQuery);

            return customers;
        }

        public async Task<Customer> GetById(long categoryId)
        {
            using var con = _factory.GetDbConnection;

            var query = $"SELECT * FROM Customers WHERE Id = {categoryId};";

            var customer = await con.QueryFirstOrDefaultAsync<Customer>(query);

            return customer;
        }

        public async Task<ServiceResponse> InsertProducts(IEnumerable<Customer> customers)
        {
            using var con = _factory.GetDbConnection;

            var currentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            var query = @$"INSERT INTO Categories (Name, HierarchicalName, Level, HasChildren, IsActive, ParentId, Ancestry, CreatedBy, CreatedDate)
                                           VALUES(@Name, @HierarchicalName, @Level, @HasChildren, @IsActive, @ParentId, @Ancestry, 1, '{currentDate}');";

            var result = await con.ExecuteAsync(query, customers);

            return new ServiceResponse
            {
                Metadata = new Metadata
                {
                    Message = "Success",
                    Status = System.Net.HttpStatusCode.OK
                },
                Data = new Data
                {
                    Id = result,
                }
            };
        }
    }
}