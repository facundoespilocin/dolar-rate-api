using DollarInfo.DAL.Base;

namespace DollarInfo.DAL.Repositories.Base
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> QueryAsync(string query, object parameters);
        Task<IEnumerable<G>> QueryAsync<G>(string query, object parameters);
        Task<T> QuerySingleAsync(string query, object parameters);
        Task ExecuteAsync(string command, object parameters);
        Task ExecuteSPAsync(string sp_name, object parameters);
        Task ExecuteMultipleAsync(BaseCommand[] commands);
    }
}