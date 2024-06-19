using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace DollarInfo.DAL.Factory
{
    public class ConnectionFactory : IConnectionFactory
    {
        private string _connectionString { get; set; }
        //private string _connectionStringNoDB { get; set; }
        //private string _connectionStringInsertDb { get; set; }

        public ConnectionFactory(IConfigurationRoot configuration)
        {
            _connectionString = configuration.GetConnectionString("Localhost");
            //_connectionStringNoDB = configuration.GetConnectionString("NoDb");
            //_connectionStringInsertDb = configuration.GetConnectionString("InsertDb");
        }

        public MySqlConnection GetDbConnection
        {
            get
            {
                var connection = new MySqlConnection(_connectionString);
                connection.Open();
                return connection;
            }
        }

        //public MySqlConnection GetDynamicSqlConnection(string databaseName)
        //{
        //    var connection = new MySqlConnection(string.Format(_connectionStringInsertDb, databaseName));
        //    connection.Open();
        //    return connection;
        //}

        //public MySqlConnection GetDbConnectionWithoutDatabase
        //{
        //    get
        //    {
        //        var connection = new MySqlConnection(_connectionStringNoDB);
        //        connection.Open();
        //        return connection;
        //    }
        //}
    }
}