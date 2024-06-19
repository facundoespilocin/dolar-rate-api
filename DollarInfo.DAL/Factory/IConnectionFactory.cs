using MySql.Data.MySqlClient;

namespace DollarInfo.DAL.Factory
{
    public interface IConnectionFactory
    {
        MySqlConnection GetDbConnection { get; }
        //MySqlConnection GetDbConnectionWithoutDatabase { get; }
        //MySqlConnection GetDynamicSqlConnection(string databaseName);
    }
}