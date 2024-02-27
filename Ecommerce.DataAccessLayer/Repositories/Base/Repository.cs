using Dapper;
using Ecommerce.DataAccessLayer.Base;
using Ecommerce.DataAccessLayer.Factory;
using System.Data;

namespace Ecommerce.DataAccessLayer.Repositories.Base
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected IConnectionFactory _factory { get; set; }

        public Repository(IConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<T>> QueryAsync(string query, object parameters)
        {
            using (var connection = _factory.GetDbConnection)
            {
                return await connection.QueryAsync<T>(query, parameters);
            }
        }

        public async Task<IEnumerable<G>> QueryAsync<G>(string query, object parameters)
        {
            using (var connection = _factory.GetDbConnection)
            {
                return await connection.QueryAsync<G>(query, parameters);
            }
        }

        public async Task<G> QuerySingleOrDefaultAsync<G>(string query, object parameters)
        {
            using (var connection = _factory.GetDbConnection)
            {
                return await connection.QuerySingleOrDefaultAsync<G>(query, parameters);
            }
        }

        public async Task<T> QuerySingleAsync(string query, object parameters)
        {
            using (var connection = _factory.GetDbConnection)
            {
                return await connection.QuerySingleOrDefaultAsync<T>(query, parameters);
            }
        }

        public async Task ExecuteAsync(string command, object parameters)
        {
            using (var connection = _factory.GetDbConnection)
            {
                await connection.ExecuteAsync(command, parameters);
            }
        }
        public async Task ExecuteSPAsync(string sp_name, object parameters)
        {
            using (var connection = _factory.GetDbConnection)
            {
                await connection.ExecuteAsync(sp_name, parameters, commandTimeout: 120, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task ExecuteMultipleAsync(BaseCommand[] commands)
        {
            using (var connection = _factory.GetDbConnection)
            {
                using (var transaction = connection.BeginTransaction())
                {
                    foreach (var command in commands)
                    {
                        await connection.ExecuteAsync(command.Sql, command.Parameters, transaction);
                    }

                    transaction.Commit();
                }
            }
        }
    }
}