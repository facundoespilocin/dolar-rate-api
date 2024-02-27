using MySql.Data.MySqlClient;

namespace Ecommerce.DataAccessLayer.Factory
{
    public interface IConnectionFactory
    {
        MySqlConnection GetDbConnection { get; }
        //MySqlConnection GetDbConnectionWithoutDatabase { get; }
        //MySqlConnection GetDynamicSqlConnection(string databaseName);
    }
}